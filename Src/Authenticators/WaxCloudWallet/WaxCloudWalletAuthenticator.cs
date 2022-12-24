using System;
using System.Threading.Tasks;
using eossharp.EosSharp.EosSharp.Unity3D;
using UnityEngine;

namespace UniversalAuthenticatorLibrary.Src.Authenticators.WaxCloudWallet
{
    [Serializable]
    public class WaxCloudWalletConfig
    {
        [Space(20)] [Header("-----           WebGL Only           -----")] [Space(10)]

        #region WebGl only

        public string RpcAddress;

        public bool TryAutoLogin = false;
        public string WaxSigningURL = null;
        public string WaxAutoSigningURL = null;

        #endregion

        [Space(20)]
        [Header("-----       Desktop and Mobile       -----")]
        [Space(10)]

        #region Desktop and Mobile

        [Tooltip("Port the local Webserver will run on (also used for Callbacks)")]
        public uint LocalPort;

        [Tooltip(
            "URL of the Website implementing WCW, must be either localhost or some remote URL (without SSL! and with Custom script deployed!) ")]
        public string SigningWebsiteUrl;

        [Tooltip(
            "Wether the Website should be hosted on the local device by the local Webserver or if a remote Website (without SSL! and with Custom script deployed!) is used")]
        public bool HostLocalWebsite;

        #endregion

        [Space(20)]
        [Header("-----          Mobile Only          -----")]
        [Header("(if Website implementing WCW should be hosted on Local Device)")]
        [Space(10)]
        [Tooltip(
            "If Host Local Website set to true, the index.html and waxjs.js have to be provided as string. Note that '//'-comments will result in failure, only use '/* ... */'-Comments ")]

        #region MobileOnly and only if hosted on Device

        public string IndexHtmlString;

        [Tooltip(
            "If Host Local Website set to true, the index.html and waxjs.js have to be provided as string. Note that '//'-comments will result in failure, only use '/* ... */'-Comments ")]
        public string WaxJsHtmlString;

        #endregion

        [Space(20)]
        [Header("-----          Desktop Only        -----")]
        [Header("(if Website implementing WCW should be hosted on Local Device)")]
        [Space(10)]

        #region Desktop Only

        [Tooltip("Relative Path to index.html starting from Application.dataPath")]
        public string IndexHtmlPath;

        [Tooltip("Path to waxjs.js starting from Application.dataPath")]
        public string WaxJsPath;

        #endregion
    }

    public class WaxCloudWalletAuthenticator : Authenticator
    {
        public WaxCloudWalletConfig WaxCloudWalletConfig;

        private WaxCloudWalletPlugin _waxCloudWalletPlugin;

        private WaxCloudWalletUser _user;

        public WaxCloudWalletAuthenticator(Chain chain, UALOptions options) : base(chain, options)
        {
            Init(chain, options);
        }

        public sealed override void Init(Chain chain, UALOptions options)
        {
            _waxCloudWalletPlugin = new GameObject(nameof(WaxCloudWalletPlugin)).AddComponent<WaxCloudWalletPlugin>();

#if UNITY_EDITOR || UNITY_STANDALONE
            _waxCloudWalletPlugin.InitializeDesktop(WaxCloudWalletConfig.LocalPort,
                WaxCloudWalletConfig.SigningWebsiteUrl,
                WaxCloudWalletConfig.HostLocalWebsite, $"{Application.dataPath}/{WaxCloudWalletConfig.IndexHtmlPath}",
                $"{Application.dataPath}/{WaxCloudWalletConfig.WaxJsPath}");
#elif UNITY_WEBGL
        _waxCloudWalletPlugin.InitializeWebGl(
            !string.IsNullOrEmpty(WaxCloudWalletConfig.RpcAddress)
                ? WaxCloudWalletConfig.RpcAddress
                : chain.RpcEndpoints[0].HttpEndpoint, WaxCloudWalletConfig.TryAutoLogin,
            WaxCloudWalletConfig.WaxSigningURL, WaxCloudWalletConfig.WaxAutoSigningURL);
#elif UNITY_ANDROID || UNITY_IOS
        _waxCloudWalletPlugin.InitializeMobile(WaxCloudWalletConfig.LocalPort, WaxCloudWalletConfig.SigningWebsiteUrl,
            WaxCloudWalletConfig.HostLocalWebsite, WaxCloudWalletConfig.IndexHtmlString,
            WaxCloudWalletConfig.WaxJsHtmlString);
#endif
            _waxCloudWalletPlugin.OnLoggedIn = loginEvent =>
            {
                _user = new WaxCloudWalletUser(loginEvent.Account, _waxCloudWalletPlugin);
            };
        }

        public override async Task<User> Login(string accountName = null)
        {
            _waxCloudWalletPlugin.Login();

            int i = 0;
            while (_user == null && i < 200)
            {
                await AsyncHelper.Delay(100);
                i++;
            }

            await AsyncHelper.Delay(100);

            return _user;
        }

        public override async Task Logout()
        {
            // TODO reset WcwPlugin
            _user = null;
        }

        public override bool ShouldAutoLogin()
        {
            return false;
        }

        public override bool ShouldRender()
        {
            return true;
        }
    }
}