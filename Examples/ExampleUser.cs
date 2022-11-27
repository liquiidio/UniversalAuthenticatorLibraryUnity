using EosSharp.Core.Api.v1;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExampleUser : User
{
    public override Task<string> GetAccountName()
    {
        throw new System.NotImplementedException();
    }

    public override Task<string> GetChainId()
    {
        throw new System.NotImplementedException();
    }

    public override Task<string> GetKeys()
    {
        throw new System.NotImplementedException();
    }

    public override Task<SignTransactionResponse> SignTransaction(Transaction transaction, ISignTransactionConfig config = null)
    {
        throw new System.NotImplementedException();
    }

    public override Task<SignTransactionResponse> SignTransaction(Action[] actions, ISignTransactionConfig config)
    {
        throw new System.NotImplementedException();
    }
}
