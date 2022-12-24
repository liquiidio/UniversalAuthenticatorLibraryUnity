using System;
using System.Threading.Tasks;
using AnchorLinkSharp;
using AnchorLinkTransportSharp.Src;
using EosioSigningRequest;
using EosSharp.Core.Api.v1;

namespace UniversalAuthenticatorLibrary.Src.Authenticators.Anchor
{
    [Serializable]
    public class AnchorWalletConfig
    {
        public bool StoreSession;
    }


    public class AnchorAuthenticator : Authenticator
    {
        public UnityTransport Transport;

        private AnchorLink _link;
        private string _identifier;

        private AnchorUser _user;

        public AnchorAuthenticator(Chain chain, UALOptions options) : base(chain, options)
        {
            Init(chain, options);
        }

        public sealed override void Init(Chain chain, UALOptions options)
        {
            _identifier = options.Identifier;
            _link = new AnchorLink(new LinkOptions()
            {
                ChainId = chain.ChainId,
                Rpc = chain.RpcEndpoints[0].HttpEndpoint,
                Transport = Transport
            });
        }

        public override async Task<User> Login(string accountName = null)
        {
            var session = await _link.RestoreSession(_identifier, new PermissionLevel()
            {
                actor = accountName,
                permission = SigningRequestConstants.PlaceholderPermission
            });

            if (session == null)
            {
                var identifyResult = await _link.Login(_identifier);
                session = identifyResult.Session;
            }
            _user = new AnchorUser(session); 
            return _user;
        }

        public override async Task Logout()
        {
            await _user.Session.Remove();
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