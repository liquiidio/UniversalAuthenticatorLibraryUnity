using AnchorLinkSharp;
using EosSharp.Core.Api.v1;
using System.Threading.Tasks;
using Action = EosSharp.Core.Api.v1.Action;

namespace UniversalAuthenticatorLibrary.Src.Authenticators.Anchor
{
    public class AnchorUser : User
    {
        public LinkSession Session { get; }

        public AnchorUser(LinkSession session)
        {
            this.Session = session;
        }

        public override async Task<string> GetAccountName()
        {
            return Session.Auth.actor;
        }

        public override async Task<string> GetChainId()
        {
            return Session.AnchorLink.ChainId;
        }

        public override async Task<string> GetKeys()
        {
            return Session.PublicKey;
        }

        public override string GetWalletType()
        {
            return "Anchor";
        }

        public override async Task<SignTransactionResponse> SignTransaction(Transaction transaction,
            SignTransactionConfig config = null)
        {
            return await Transact(null, null, transaction);
        }

        public override async Task<SignTransactionResponse> SignTransaction(Action[] actions,
            SignTransactionConfig config = null)
        {
            return await Transact(null, actions, null);
        }

        private async Task<SignTransactionResponse> Transact(Action action, Action[] actions, Transaction transaction)
        {
            UalError ualError;
            try
            {
                var transactResult = await Session.Transact(new TransactArgs()
                    {Action = action, Actions = actions, Transaction = transaction});
                return new SignTransactionResponse()
                {
                    Transaction = transactResult.Processed,
                    Status = "",
                };
            }
            catch (EosSharp.Core.Exceptions.ApiErrorException e)
            {
                ualError = new UalError()
                {
                    Code = e.code.ToString(),
                    Message = e.message,
                    Name = e.error.name
                };
            }
            catch (EosSharp.Core.Exceptions.ApiException e)
            {
                ualError = new UalError()
                {
                    Code = e.StatusCode.ToString(),
                    Message = e.Message
                };
            }

            return new SignTransactionResponse()
            {
                Status = "",
                UalError = ualError
            };
        }
    }
}