using System.Collections.Generic;

namespace UniversalAuthenticatorLibrary
{
    public static class UalConstants
    {
        public const string SESSION_ACCOUNT_NAME_KEY = "UALSharp.SESSION_ACCOUNT_NAME_KEY";
        public const string SESSION_EXPIRATION_KEY = "UALSharp.SESSION_EXPIRATION_KEY";
        public const string SESSION_AUTHENTICATOR_KEY = "UALSharp.SESSION_AUTHENTICATOR_KEY";

        public static List<string> Keys => new List<string>()
            {SESSION_ACCOUNT_NAME_KEY, SESSION_EXPIRATION_KEY, SESSION_AUTHENTICATOR_KEY};
    }
}