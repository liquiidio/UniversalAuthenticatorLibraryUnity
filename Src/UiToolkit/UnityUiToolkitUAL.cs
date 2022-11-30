using System.Collections;
using System.Collections.Generic;
using Assets.Packages.UniversalAuthenticatorLibrarySharp.Src.UiToolkit.Ui;
using System;
using UnityEngine;

public class UnityUiToolkitUAL : UnityUAL
{
    [SerializeField] internal AuthenticatorsPanel AuthenticatorsPanel;
    [SerializeField] internal AuthenticatorButtonItem AuthenticatorButtonItem;
    
    public UnityUiToolkitUAL(Chain chain, UALOptions ualOptions, List<Authenticator> authenticators) : base(chain, ualOptions, authenticators)
    {
    }

    protected override void CreateUalPanel(Authenticator[] authenticators)
    {
        AuthenticatorsPanel.Show();

        foreach (var authenticator in authenticators)
        {
            // Has Icon, Style, TextColor etc.
            var buttonStyle = authenticator.GetStyle();

            AuthenticatorsPanel._authenticatorButtonBox.Add(AuthenticatorButtonItem.Clone(buttonStyle, () =>
            {
               LoginUser(authenticator);
               AuthenticatorsPanel.Hide();
            }));
        }
    }
}
