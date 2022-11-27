using System;
using AnchorLinkSharp;
using EosSharp.Core.Api.v1;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Action = EosSharp.Core.Api.v1.Action;

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

    public override async Task<SignTransactionResponse> SignTransaction(Transaction transaction, ISignTransactionConfig config = null)
    {
        return await Transact(null, null, transaction);
    }

    public override async Task<SignTransactionResponse> SignTransaction(Action[] actions, ISignTransactionConfig config = null)
    {
        return await Transact(null, actions, null);
    }

    private async Task<SignTransactionResponse> Transact(Action action, Action[] actions, Transaction transaction)
    {
        UalError ualError;
        try
        {
            var transactResult = await Session.Transact(new TransactArgs() { Action = null, Actions = null, Transaction = transaction });
            return new SignTransactionResponse()
            {
                transaction = transactResult.Processed,
                status = "",
            };
        }
        catch (EosSharp.Core.Exceptions.ApiErrorException e)
        {
            ualError = new UalError()
            {
                code = e.code.ToString(),
                message = e.message,
                name = e.error.name
            };
        }
        catch (EosSharp.Core.Exceptions.ApiException e)
        {
            ualError = new UalError()
            {
                code = e.StatusCode.ToString(),
                message = e.Message
            };
        }

        return new SignTransactionResponse()
        {
            status = "",
            UalError = ualError
        };
    }
}
