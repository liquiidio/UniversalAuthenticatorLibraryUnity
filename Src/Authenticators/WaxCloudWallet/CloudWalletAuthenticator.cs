using System;
using System.Threading.Tasks;
using Assets.Packages.CloudWalletUnity.Src;
using eossharp.EosSharp.EosSharp.Unity3D;
using UnityEngine;

namespace UniversalAuthenticatorLibrary.Src.Authenticators.WaxCloudWallet
{
    [Serializable]
    public class CloudWalletConfig
    {
        [Space(20)] [Header("-----           WebGL Only           -----")] [Space(10)]

        #region WebGl only

        public string RpcAddress;

        public bool TryAutoLogin = false;

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

    public class CloudWalletAuthenticator : Authenticator
    {
        public CloudWalletConfig CloudWalletConfig;

        private CloudWalletPlugin _waxCloudWalletPlugin;

        private CloudWalletUser _user;

        public CloudWalletAuthenticator(Chain chain, UALOptions options) : base(chain, options)
        {
            Init(chain, options);
        }

        public sealed override void Init(Chain chain, UALOptions options)
        {
            _waxCloudWalletPlugin = new GameObject(nameof(CloudWalletPlugin)).AddComponent<CloudWalletPlugin>();

#if (UNITY_EDITOR || UNITY_STANDALONE) && !UNITY_WEBGL && !UNITY_ANDROID && !UNITY_IOS
            _waxCloudWalletPlugin.InitializeDesktop(CloudWalletConfig.LocalPort,
                CloudWalletConfig.SigningWebsiteUrl,
                CloudWalletConfig.HostLocalWebsite, $"{Application.dataPath}/{CloudWalletConfig.IndexHtmlPath}",
                $"{Application.dataPath}/{CloudWalletConfig.WaxJsPath}");
#elif UNITY_WEBGL
        _waxCloudWalletPlugin.InitializeWebGl(
            !string.IsNullOrEmpty(CloudWalletConfig.RpcAddress)
                ? CloudWalletConfig.RpcAddress
                : chain.RpcEndpoints[0].HttpEndpoint, CloudWalletConfig.TryAutoLogin);
#elif UNITY_ANDROID || UNITY_IOS
        _waxCloudWalletPlugin.InitializeMobile(CloudWalletConfig.LocalPort, CloudWalletConfig.SigningWebsiteUrl,
            CloudWalletConfig.HostLocalWebsite, CloudWalletConfig.IndexHtmlString,
            CloudWalletConfig.WaxJsHtmlString);
#endif
            _waxCloudWalletPlugin.OnLoggedIn = loginEvent =>
            {
                _user = new CloudWalletUser(loginEvent.Account, _waxCloudWalletPlugin);
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