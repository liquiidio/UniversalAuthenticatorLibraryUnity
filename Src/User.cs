using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EosSharp;
using EosSharp.Core.Api.v1;
using System.Threading.Tasks;

namespace UniversalAuthenticatorLibrary
{
    public abstract class User
    {
        /**
      * @param transaction  The transaction to be signed (a object that matches the RpcAPI structure).
      */
        public abstract Task<SignTransactionResponse> SignTransaction(Transaction transaction,
            SignTransactionConfig config = null);

        /**
      * @param actions   The actions to be included into the transaction to be signed (a object that matches the RpcAPI structure).
      */
        public abstract Task<SignTransactionResponse> SignTransaction(Action[] actions,
            SignTransactionConfig config = null);

        //  /**
        //   * @param publicKey   The public key to use for signing.
        //   * @param data        The data to be signed.
        //   * @param helpText    Help text to explain the need for arbitrary data to be signed.
        //   *
        //   * @returns           The signature
        //   */
        //  public abstract signArbitrary(publicKey: string, data: string, helpText: string) : Promise<string>

        //  /**
        //   * @param challenge   Challenge text sent to the authenticator.
        //   *
        //   * @returns           Whether the user owns the private keys corresponding with provided public keys.
        //   */
        //  public abstract verifyKeyOwnership(challenge: string) : Promise<boolean>

        public abstract Task<string> GetAccountName();

        public abstract Task<string> GetChainId();

        public abstract Task<string> GetKeys();

        public abstract string GetWalletType();

        //  protected returnEosjsTransaction(wasBroadcast: boolean, completedTransaction: any) : SignTransactionResponse {
        //    if (wasBroadcast) {
        //      if (completedTransaction.hasOwnProperty('transaction_id')) {
        //        return {
        //          wasBroadcast: true,
        //          transactionId: completedTransaction.transaction_id,
        //          status: completedTransaction.processed.receipt.status,
        //          transaction: completedTransaction,
        //        }
        //      } else if (completedTransaction.hasOwnProperty('code'))
        //{
        //    return {
        //    wasBroadcast: true,
        //          error:
        //        {
        //        code: completedTransaction.code,
        //            message: completedTransaction.message,
        //            name: completedTransaction.error.name,
        //          },
        //          transaction: completedTransaction,
        //        }
        //}
        //else
        //{
        //    return {
        //    wasBroadcast: true,
        //          transaction: completedTransaction,
        //        }
        //}
        //    } else
        //{
        //    return {
        //    wasBroadcast: false,
        //        transaction: completedTransaction,
        //      }
        //}
        //  }

        protected string BuildRpcEndpoint(RpcEndpoint endPoint)
        {
            var rpcEndpointString = $"{endPoint.Protocol}://${endPoint.Host}:${endPoint.Port}";
            if (!string.IsNullOrEmpty(endPoint.Path))
            {
                var separator = "/";
                if (endPoint.Path.StartsWith("/"))
                {
                    separator = "";
                }

                rpcEndpointString = $"{rpcEndpointString}${separator}${endPoint.Path}";
            }

            if (rpcEndpointString.EndsWith("/"))
            {
                rpcEndpointString = rpcEndpointString.Substring(0, rpcEndpointString.Length - 1);
            }

            return rpcEndpointString;
        }
    }
}