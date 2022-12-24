using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using eossharp.EosSharp.EosSharp.Unity3D;
using UnityEngine;

namespace UniversalAuthenticatorLibrary
{
    // based on https://github.com/EOSIO/ual-plainjs-renderer/blob/master/src/UALJs.ts
    public abstract class UnityUAL : MonoBehaviour
    {
        public Chain Chain;
        public UALOptions UalOptions;
        public List<Authenticator> Authenticators;

        public Action<User> OnUserLogin;

        /**
     * @param chains          A list of chains the dapp supports.
     *
     * @param appName         The name of the app using the authenticators
     *
     * @param authenticators  A list of authenticator apps that the dapp supports.
     */
        public UnityUAL(Chain chain, UALOptions ualOptions, List<Authenticator> authenticators)
        {
            Chain = chain;
            UalOptions = ualOptions;
            Authenticators = authenticators;
        }

        public bool IsAutologin { get; private set; }
        public Authenticator ActiveAuthenticator { get; private set; }

        /**
     * Returns an object with a list of initialized Authenticators that returned true for shouldRender()
     * as well as an authenticator that supports autoLogin
     */
        public AuthenticatorResponse GetAuthenticators()
        {
            return new AuthenticatorResponse()
            {
                AvailableAuthenticators = Authenticators.Where(a => a.ShouldRender()).ToArray(),
                AutoLoginAuthenticator = Authenticators.FirstOrDefault(a => a.ShouldRender() && a.ShouldAutoLogin())
            };
        }

        /**
     * Initializes UAL: If a renderConfig was provided and no autologin authenticator
     * is returned it will render the Auth Button and relevant DOM elements.
     *
     */
        public async Task Init()
        {
            // we need to wait a little until we can Init the UAL as the Authenticators need to Initialize first
            // as alternative we could change the Script Execution Order
            await AsyncHelper.Delay(250);

            foreach (var authenticator in Authenticators)
            {
                authenticator.Init(Chain, new UALOptions()
                {
                    Identifier = UalOptions.Identifier
                });
            }

            var authenticators = GetAuthenticators();

            // perform this check first, if we're autologging in we don't render a dom
            if (authenticators.AutoLoginAuthenticator != null)
            {
                IsAutologin = true;
                await LoginUser(authenticators.AutoLoginAuthenticator);
                ActiveAuthenticator = authenticators.AutoLoginAuthenticator;
            }
            else
            {
                // check for existing session and resume if possible
                await AttemptSessionLogin(authenticators.AvailableAuthenticators);

                CreateUalPanel(authenticators.AvailableAuthenticators);
            }
        }

        // override in Canvas/UiToolkit
        protected abstract void CreateUalPanel(Authenticator[] authenticators);

        private async Task AttemptSessionLogin(Authenticator[] availableAuthenticators)
        {
            var sessionExpiration = PlayerPrefs.GetString(UalConstants.SESSION_EXPIRATION_KEY);
            if (!string.IsNullOrEmpty(sessionExpiration))
            {
                // clear session if it has expired and continue
                if (DateTime.TryParse(sessionExpiration, out var expiration) && expiration <= DateTime.Now)
                {
                    ClearStorageKeys();
                }
                else
                {
                    var authenticatorName = PlayerPrefs.GetString(UalConstants.SESSION_AUTHENTICATOR_KEY);
                    var sessionAuthenticator =
                        Authenticators.FirstOrDefault(a => a.GetType().Name == authenticatorName);
                    var accountName = PlayerPrefs.GetString(UalConstants.SESSION_ACCOUNT_NAME_KEY);
                    await LoginUser(sessionAuthenticator, accountName);
                }
            }
        }

        internal async Task LoginUser(Authenticator authenticator, string accountName = null)
        {
            User user;

            // set the active authenticator so we can use it in logout
            ActiveAuthenticator = authenticator;

            var invalidateSeconds = ActiveAuthenticator.ShouldInvalidateAfter();
            var invalidateAt = DateTime.Now;
            invalidateAt = invalidateAt.AddSeconds(invalidateSeconds);

            PlayerPrefs.SetString(UalConstants.SESSION_EXPIRATION_KEY,
                invalidateAt.ToString(CultureInfo.InvariantCulture));
            PlayerPrefs.SetString(UalConstants.SESSION_AUTHENTICATOR_KEY, authenticator.GetType().Name);

            try
            {
                if (!string.IsNullOrEmpty(accountName))
                {
                    user = await authenticator.Login(accountName);

                    PlayerPrefs.SetString(UalConstants.SESSION_ACCOUNT_NAME_KEY, accountName);
                }
                else
                {
                    user = await authenticator.Login();
                }

                OnUserLogin?.Invoke(user);

            }
            catch (Exception e)
            {
                Debug.LogError(e);
                ClearStorageKeys();
                throw e;
            }

            // reset our modal state if we're not autologged in (no dom is rendered for autologin)
            if (!this.IsAutologin)
            {
                // TODO
                //this.dom!.reset()
            }
        }

        private void ClearStorageKeys()
        {
            foreach (var key in UalConstants.Keys)
            {
                PlayerPrefs.DeleteKey(key);
            }
        }
    }
}