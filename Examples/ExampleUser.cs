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

    public override string GetWalletType()
    {
        throw new System.NotImplementedException();
    }

    public override Task<SignTransactionResponse> SignTransaction(Transaction transaction, SignTransactionConfig config = null)
    {
        throw new System.NotImplementedException();
    }

    public override Task<SignTransactionResponse> SignTransaction(Action[] actions, SignTransactionConfig config)
    {
        throw new System.NotImplementedException();
    }
}
