using System;
using EosSharp.Core.Api.v1;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Packages.eossharp.EosSharp.EosSharp.Unity3D;
using EosSharp.Core;
using EosSharp.Unity3D;
using UnityEngine;
using Action = EosSharp.Core.Api.v1.Action;

public class WombatUser : User
{
#if !UNITY_WEBGL
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
            ChainId = wombatPlugin.Network.ChainId,
            HttpEndpoint = wombatPlugin.Network.HttpEndpoint
        },
        new EosSharp.Unity3D.HttpHandler()
        );
    }

    public override async Task<string> GetAccountName()
    {
        return AccountName;
    }

    public override async Task<string> GetChainId()
    {
        return _wombatPlugin.Network.ChainId;
    }

    public override async Task<string> GetKeys()
    {
        return (await _api.GetAccount(new GetAccountRequest()
        {
            account_name = AccountName
        })).permissions.SingleOrDefault(p => p.perm_name == "active")?.required_auth.keys.First().key;
    }

    public override string GetWalletType()
    {
        return "Wombat";
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
            await AsyncHelper.Delay(100);
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
#else
    private string account;
    public WombatUser(string account, object wombatPlugin)
    {
        this.account = account;
    }

    public override Task<SignTransactionResponse> SignTransaction(Transaction transaction, SignTransactionConfig config = null)
    {
        throw new NotImplementedException("Wombat is not supported on Platforms other than WebGL");
    }

    public override Task<SignTransactionResponse> SignTransaction(Action[] actions, SignTransactionConfig config = null)
    {
        throw new NotImplementedException("Wombat is not supported on Platforms other than WebGL");
    }

    public override Task<string> GetAccountName()
    {
        throw new NotImplementedException("Wombat is not supported on Platforms other than WebGL");
    }

    public override Task<string> GetChainId()
    {
        throw new NotImplementedException("Wombat is not supported on Platforms other than WebGL");
    }

    public override Task<string> GetKeys()
    {
        throw new NotImplementedException("Wombat is not supported on Platforms other than WebGL");
    }

    public override string GetWalletType()
    {
        return "Wombat";
    }
#endif
}
