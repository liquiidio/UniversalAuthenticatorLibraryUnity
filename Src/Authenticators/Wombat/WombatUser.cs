using EosSharp.Core.Api.v1;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using EosSharp.Core;
using EosSharp.Unity3D;
using UnityEngine;

public class WombatUser : User
{
    private WombatPlugin _wombatPlugin;
    public string AccountName { get; }

    private WombatSignEvent _wombatSignEvent;
    private WombatErrorEvent _wombatErrorEvent;

    private EosApi _api;

    public WombatUser(string accountName, WombatPlugin wombatPlugin)
    {
        this.AccountName = accountName;
        this._wombatPlugin = wombatPlugin;
        this._wombatPlugin.OnTransactionSigned = wcwSignEvent =>
        {
            _wombatSignEvent = wcwSignEvent;
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
        // TODO
        return "1064487b3cd1a897ce03ae5b6a865651747e2e152090f99c1d19d44e01aea5a4";
    }

    public override async Task<string> GetKeys()
    {
        // TODO
        return (await _api.GetAccount(new GetAccountRequest()
        {
            account_name = AccountName
        })).permissions.SingleOrDefault(p => p.perm_name == "active")?.required_auth.keys.First().key;
    }

    public override async Task<SignTransactionResponse> SignTransaction(Transaction transaction, SignTransactionConfig config = null)
    {
        _wombatPlugin.Sign(transaction.actions.ToArray());
        return await WaitForEvent();
    }

    public override async Task<SignTransactionResponse> SignTransaction(Action[] actions, SignTransactionConfig config = null)
    {
        _wombatPlugin.Sign(actions);
        return await WaitForEvent();
    }

    private async Task<SignTransactionResponse> WaitForEvent()
    {
        int i = 0;
        while (_wombatSignEvent == null && _wombatErrorEvent != null && i < 200)
        {
            await UniTask.Delay(100);
            i++;
        }


        if (_wombatSignEvent != null)
        {
            return new SignTransactionResponse()
            {
                Transaction = _wombatSignEvent.Result.processed,
                Status = "",
            };
        }
        else if (_wombatErrorEvent != null)
        {
            UalError ualError;
            ualError = new UalError()
            {
                Message = _wombatErrorEvent.Message
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
                Status = "",
                UalError = ualError
            };
        }
        else
        {
            return new SignTransactionResponse()
            {
                UalError = new UalError()
                {
                    Message = "Timed out"
                }
            };
        }
    }
}
