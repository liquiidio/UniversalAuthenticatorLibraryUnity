using System.Collections.Generic;
using UniversalAuthenticatorLibrary.Src.UiToolkit.Ui;
using UnityEngine;

namespace UniversalAuthenticatorLibrary.Src.UiToolkit
{
    public class UnityUiToolkitUAL : UnityUAL
    {
        [SerializeField] internal AuthenticatorsPanel AuthenticatorsPanel;

        public UnityUiToolkitUAL(Chain chain, UALOptions ualOptions, List<Authenticator> authenticators) : base(chain,
            ualOptions, authenticators)
        {
        }

        /// <summary>
        /// Show the authenticator panel and populate it with the declared authenticator prefabs 
        /// </summary>
        /// <param name="authenticators"></param>
        protected override void CreateUalPanel(Authenticator[] authenticators)
        {
            Debug.Log("AuthenticatorsPanel.Show()");
            AuthenticatorsPanel.Show();
            Debug.Log("AuthenticatorsPanel.AuthenticatorButtonBox.Clear()");
            AuthenticatorsPanel.AuthenticatorButtonBox?.Clear();

            Debug.Log("foreach (var authenticator in authenticators)");
            foreach (var authenticator in authenticators)
            {
                Debug.Log("AuthenticatorsPanel.AuthenticatorButtonItem.Clone()");
                var authenticatorButton = AuthenticatorsPanel.AuthenticatorButtonItem.Clone(authenticator.GetStyle(),
                    async () =>
                    {
                        AuthenticatorsPanel.Hide();
                        await LoginUser(authenticator);
                        //await authenticator.Login();
                    });

                Debug.Log("AuthenticatorsPanel.AuthenticatorButtonBox.Add()");
                AuthenticatorsPanel.AuthenticatorButtonBox?.Add(authenticatorButton);
            }
        }
    }

}
