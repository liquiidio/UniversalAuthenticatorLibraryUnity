using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UniversalAuthenticatorLibrary
{
    public class UAL
    {
        public Chain[] Chains;
        public string AppName;
        public Authenticator[] Authenticators;

        /**
         * @param chains          A list of chains the dapp supports.
         *
         * @param appName         The name of the app using the authenticators
         *
         * @param authenticators  A list of authenticator apps that the dapp supports.
         */
        public UAL(Chain[] chains, string appName, Authenticator[] authenticators)
        {
            Chains = chains;
            AppName = appName;
            Authenticators = authenticators;
        }

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
    }
}