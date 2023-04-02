using AnchorLinkTransportSharp.Src.Transports.Canvas;
using EosSharp.Core.Api.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniversalAuthenticatorLibrary.Src.Authenticators.Anchor;
using UniversalAuthenticatorLibrary.Src.Canvas;

namespace UniversalAuthenticatorLibrary.Examples.Canvas
{
    public class UALCanvasExample : MonoBehaviour
    {
        public UnityCanvasUAL UnityCanvasUAL;

        public GameObject TransactionPanel;

        private User user;
        [SerializeField]private EventSystem _canvasEventSystem;

        async void Start()
        {
            if (SceneManager.sceneCount > 1)
                UnityCanvasUAL.AuthenticatorPanel.GetComponentInChildren<Button>().gameObject.SetActive(false);

            await UnityCanvasUAL.Init();
            UnityCanvasUAL.OnUserLogin += UserLogin;

            BindButtons();
        }

        private void BindButtons()
        {
            var _anchorCanvasTransport =
                (UnityCanvasUAL.Authenticators.First(_auth => _auth.GetType() == typeof(AnchorAuthenticator)) as
                    AnchorAuthenticator).Transport as UnityCanvasTransport;

            _anchorCanvasTransport.LoginPanel.transform.Find("HeaderBorder/CloseLoginPanelButton")
                .GetComponent<Button>().onClick.AddListener(delegate
                {
                    _anchorCanvasTransport.DisableAllPanels();
                    ReturnToAuthenticatorSelection();
                });

            _anchorCanvasTransport.LoadingPanel.transform.Find("HeaderBorder/CloseLoadingPanelButton")
                .GetComponent<Button>().onClick.AddListener(delegate
                {
                    _anchorCanvasTransport.DisableAllPanels();
                    ReturnToAuthenticatorSelection();
                });

            _anchorCanvasTransport.SignPanel.transform.Find("HeaderBorder/CloseSignPanelButton").GetComponent<Button>()
                .onClick.AddListener(delegate
                {
                    _anchorCanvasTransport.DisableAllPanels();
                    ReturnToAuthenticatorSelection();
                });

            _anchorCanvasTransport.SuccessPanel.transform.Find("HeaderBorder/CloseSuccessPanelButton")
                .GetComponent<Button>().onClick.AddListener(delegate
                {
                    _anchorCanvasTransport.DisableAllPanels();
                    ReturnToAuthenticatorSelection();
                });

            _anchorCanvasTransport.FailurePanel.transform.Find("HeaderBorder/CloseFailurePanelButton")
                .GetComponent<Button>().onClick.AddListener(delegate
                {
                    _anchorCanvasTransport.DisableAllPanels();
                    ReturnToAuthenticatorSelection();
                });

            _anchorCanvasTransport.TimeoutPanel.transform.Find("HeaderBorder/CloseTimeoutPanelButton")
                .GetComponent<Button>().onClick.AddListener(delegate
                {
                    _anchorCanvasTransport.DisableAllPanels();
                    ReturnToAuthenticatorSelection();
                });
        }

        private void ReturnToAuthenticatorSelection()
        {
            TransactionPanel.SetActive(false);
            UnityCanvasUAL.AuthenticatorPanel.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                OnBrowserClipboardPaste("Working as intended!");
            }
        }

        /// <summary>Called when ctrl + v is pressed in browser (webgl)</summary>
        /// <param name="pastedText">The pasted text.</param>
        public void OnBrowserClipboardPaste(string pastedText)
        {
            if (_canvasEventSystem.currentSelectedGameObject?.GetComponent<TMP_InputField>() != null)
                _canvasEventSystem.currentSelectedGameObject.GetComponent<TMP_InputField>().text = pastedText;

            print("This function to paste is being called!");
        }

        private void UserLogin(User _user)
        {
            user = _user;

            if (UnityCanvasUAL.ActiveAuthenticator.GetType() == typeof(AnchorAuthenticator))
            {
                var _anchorCanvasTransport =
                    (UnityCanvasUAL.ActiveAuthenticator as AnchorAuthenticator).Transport as UnityCanvasTransport;

                _anchorCanvasTransport.DisableAllPanels();

                _anchorCanvasTransport.SwitchToNewPanel(TransactionPanel);
            }

            else
            {
                TransactionPanel.SetActive(true); // Follow up on this
            }
        }

        /// <summary>
        /// Gather data from the custom transfer UI panel
        /// </summary>
        /// <param name="TransferDetailsPanel"></param>
        public async void TryTransferTokens(GameObject TransferDetailsPanel)
        {
            string _frmAcc = "";
            string _toAcc = "";
            string _qnty = "";
            string _memo = "";

            foreach (var _inputField in TransferDetailsPanel.GetComponentsInChildren<TMP_InputField>())
            {
                if (_inputField.name == "FromAccountInputField(TMP)")
                    _frmAcc = _inputField.text;

                else if (_inputField.name == "ToAccountInputField(TMP)")
                    _toAcc = _inputField.text;

                else if (_inputField.name == "QuantityAccountInputField(TMP)")
                {
                    _qnty = $"{_inputField.text} WAX";

                    _qnty = _qnty.Replace(",", ".");
                }
                else if (_inputField.name == "MemoAccountInputField(TMP)")
                    _memo = _inputField.text;
            }

            await Transfer
            (
                _frmAcc,
                _toAcc,
                _qnty,
                _memo
            );
        }

        private async Task Transfer(string frmAcc, string toAcc, string qnty, string memo)
        {
            var action = new EosSharp.Core.Api.v1.Action
            {
                account = "eosio.token",
                name = "transfer",
                authorization = new List<PermissionLevel>
                {
                    new PermissionLevel()
                    {
                        actor =
                            "............1", // ............1 will be resolved to the signing accounts permission
                        permission =
                            "............2" // ............2 will be resolved to the signing accounts authority
                    }
                },

                data = new Dictionary<string, object>
                {
                    {"from", frmAcc},
                    {"to", toAcc},
                    {"quantity", qnty},
                    {"memo", memo}
                }
            };
            try
            {
                await user.SignTransaction(new[] {action});
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }
    }
}