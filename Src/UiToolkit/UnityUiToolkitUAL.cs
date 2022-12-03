using System.Collections;
using System.Collections.Generic;
using Assets.Packages.UniversalAuthenticatorLibrary.Src.UiToolkit.Ui;
using System;
using UnityEngine;

namespace Assets.Packages.UniversalAuthenticatorLibrary.Src.UiToolkit
{
    public class UnityUiToolkitUAL : UnityUAL
    {
        [SerializeField] internal AuthenticatorsPanel AuthenticatorsPanel;
        [SerializeField] internal AuthenticatorButtonItem AuthenticatorButtonItem;

        public UnityUiToolkitUAL(Chain chain, UALOptions ualOptions, List<Authenticator> authenticators) : base(chain,
            ualOptions, authenticators)
        {
        }

        protected override void CreateUalPanel(Authenticator[] authenticators)
        {
            AuthenticatorsPanel.Show();
            AuthenticatorsPanel.AuthenticatorButtonBox.Clear();

            foreach (var authenticator in authenticators)
            {
                // Has Icon, Style, TextColor etc.
                var buttonStyle = authenticator.GetStyle();

                AuthenticatorsPanel.AuthenticatorButtonBox.Add(AuthenticatorButtonItem.Clone(buttonStyle, () =>
                {
                    LoginUser(authenticator);
                    AuthenticatorsPanel.Hide();
                }));
            }
        }
    }

}
