using Assets.Packages.AnchorLinkTransportSharp.Src.Transports.Canvas;
using Assets.Packages.UniversalAuthenticatorLibrary.Src.UiToolkit.Ui;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnityCanvasUAL : UnityUAL
{
    public GameObject AuthenticatorButtonPrefab;

    public RectTransform AuthenticatorButtonPanel;

    public RectTransform AuthenticatorPanel;

    public UnityCanvasUAL(Chain chain, UALOptions ualOptions, List<Authenticator> authenticators) : base(chain, ualOptions, authenticators)
    {
    }

    protected override void CreateUalPanel(Authenticator[] authenticators)
    {
        foreach (var authenticator in authenticators)
        {
            // Has Icon, Style, TextColor etc.
            var buttonStyle = authenticator.GetStyle();

            var _newButton = Instantiate(AuthenticatorButtonPrefab, AuthenticatorButtonPanel);

            _newButton.name = $"{buttonStyle.Text} Authenticator Button";

            _newButton.transform.GetChild(0).GetComponentInChildren<Image>().sprite = buttonStyle.Icon;
            _newButton.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
            
            _newButton.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = buttonStyle.Text;
            _newButton.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().color = buttonStyle.TextColor;

            _newButton.GetComponent<Image>().color = buttonStyle.Background;

            _newButton.GetComponent<Button>().onClick.AddListener(delegate
            {
                AuthenticatorPanel.gameObject.SetActive(false);
                LoginUser(authenticator);
            });

            // called when the specific Button is pressed
            //Action onClick = () => LoginUser(authenticator);  // Add this to button listener
        }
    }
}
