using AnchorLinkTransportSharp.Src.Transports.Canvas;
using UniversalAuthenticatorLibrary.Examples.UiToolkit;
using EosSharp.Core.Api.v1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UALCanvasExample : MonoBehaviour
{
    public UnityCanvasUAL UnityCanvasUAL;

    public GameObject TransactionPanel;

    private User user;

    // Start is called before the first frame update
    void Start()
    {
        UnityCanvasUAL.OnUserLogin += UserLogin;
        UnityCanvasUAL.Init();

        BindButtons();
    }

    private void BindButtons()
    {
        var _anchorCanvasTransport = (UnityCanvasUAL.Authenticators.First(_auth => _auth.GetType() == typeof(AnchorAuthenticator)) as AnchorAuthenticator).Transport as UnityCanvasTransport;

        _anchorCanvasTransport.LoginPanel.transform.Find("HeaderBorder/CloseLoginPanelButton").GetComponent<Button>().onClick.AddListener(delegate
        {
            _anchorCanvasTransport.DisableAllPanels();
            ReturnToAuthenticatorSelection();
        });

        _anchorCanvasTransport.LoadingPanel.transform.Find("HeaderBorder/CloseLoadingPanelButton").GetComponent<Button>().onClick.AddListener(delegate
        {
            _anchorCanvasTransport.DisableAllPanels();
            ReturnToAuthenticatorSelection();
        });

        _anchorCanvasTransport.SignPanel.transform.Find("HeaderBorder/CloseSignPanelButton").GetComponent<Button>().onClick.AddListener(delegate
        {
            _anchorCanvasTransport.DisableAllPanels();
            ReturnToAuthenticatorSelection();
        });

        _anchorCanvasTransport.SuccessPanel.transform.Find("HeaderBorder/CloseSuccessPanelButton").GetComponent<Button>().onClick.AddListener(delegate
        {
            _anchorCanvasTransport.DisableAllPanels();
            ReturnToAuthenticatorSelection();
        });

        _anchorCanvasTransport.FailurePanel.transform.Find("HeaderBorder/CloseFailurePanelButton").GetComponent<Button>().onClick.AddListener(delegate
        {
            _anchorCanvasTransport.DisableAllPanels();
            ReturnToAuthenticatorSelection();
        });

        _anchorCanvasTransport.TimeoutPanel.transform.Find("HeaderBorder/CloseTimeoutPanelButton").GetComponent<Button>().onClick.AddListener(delegate
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

    private void UserLogin(User _user)
    {
        user = _user;

        if (UnityCanvasUAL.ActiveAuthenticator.GetType() == typeof(AnchorAuthenticator))
        {
            var _anchorCanvasTransport = (UnityCanvasUAL.ActiveAuthenticator as AnchorAuthenticator).Transport as UnityCanvasTransport;

            _anchorCanvasTransport.SwitchToNewPanel(TransactionPanel);
        }
    }

    // Gather data from the custom transfer UI panel
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
                        { "from", frmAcc },
                        { "to", toAcc},
                        { "quantity", qnty},
                        { "memo", memo }
                    }
        };
        try
        {
            await user.SignTransaction(new[] { action });
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }
}
