using EosSharp.Core.Api.v1;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnchorLinkSharp;
using Cysharp.Threading.Tasks;
using EosSharp.Core;
using EosSharp.Unity3D;
using UnityEngine;

public class WaxCloudWalletUser : User
{
    private WaxCloudWalletPlugin _waxCloudWalletPlugin;
    public string AccountName { get; }

    private WcwSignEvent _wcwSignEvent;
    private WcwErrorEvent _wcwErrorEvent;

    private EosApi _api;

    public WaxCloudWalletUser(string accountName, WaxCloudWalletPlugin waxCloudWalletPlugin)
    {
        this.AccountName = accountName;
        this._waxCloudWalletPlugin = waxCloudWalletPlugin;
        this._waxCloudWalletPlugin.OnTransactionSigned = wcwSignEvent =>
        {
            _wcwSignEvent = wcwSignEvent;
        };
        _api = new EosApi(new EosConfigurator()
        {
            ChainId = "",
            HttpEndpoint = "",
        },
#if UNITY_WEBGL
        new EosSharp.Unity3D.HttpHandler()
#else
        new HttpHandler()
#endif
        );
    }

    public override async Task<string> GetAccountName()
    {
        return AccountName;
    }

    public override async Task<string> GetChainId()
    {
        return "1064487b3cd1a897ce03ae5b6a865651747e2e152090f99c1d19d44e01aea5a4";
    }

    public override async Task<string> GetKeys()
    {
        return (await _api.GetAccount(new GetAccountRequest()
        {
            account_name = AccountName
        })).permissions.SingleOrDefault(p => p.perm_name == "active")?.required_auth.keys.First().key;
    }

    public override async Task<SignTransactionResponse> SignTransaction(Transaction transaction, ISignTransactionConfig config = null)
    {
        _waxCloudWalletPlugin.Sign(transaction.actions.ToArray());
        return await WaitForEvent();
    }

    public override async Task<SignTransactionResponse> SignTransaction(Action[] actions, ISignTransactionConfig config = null)
    {
        _waxCloudWalletPlugin.Sign(actions);
        return await WaitForEvent();
    }

    private async Task<SignTransactionResponse> WaitForEvent()
    {
        int i = 0;
        while (_wcwSignEvent == null && _wcwErrorEvent != null && i < 200)
        {
            await UniTask.Delay(100);
            i++;
        }


        if (_wcwSignEvent != null)
        {
            return new SignTransactionResponse()
            {
                transaction = _wcwSignEvent.Result.processed,
                status = "",
            };
        }
        else if(_wcwErrorEvent != null)
        {
            UalError ualError;
            ualError = new UalError()
            {
                message = _wcwErrorEvent.Message
            };
            //catch (EosSharp.Core.Exceptions.ApiErrorException e)
            //{
            //    ualError = new UalError()
            //    {
            //        code = e.code.ToString(),
            //        message = e.message,
            //        name = e.error.name
            //    };
            //}
            //catch (EosSharp.Core.Exceptions.ApiException e)
            //{
            //    ualError = new UalError()
            //    {
            //        code = e.StatusCode.ToString(),
            //        message = e.Message
            //    };
            //}

            return new SignTransactionResponse()
            {
                status = "",
                UalError = ualError
            };
        }
        else
        {
            return new SignTransactionResponse()
            {
                UalError = new UalError()
                {
                    message = "Timed out"
                }
            };
        }
    }
}
