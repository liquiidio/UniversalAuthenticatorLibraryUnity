using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthenticatorResponse
{
    /** List of authenticators */
    public Authenticator[] AvailableAuthenticators { get; set; }

    /** The Authenticator to use for auto login, if supported and available */
    public Authenticator AutoLoginAuthenticator { get; set; }
}



///** The fields that an Authenticator can style on their button */
//export interface ButtonStyle
//{
//    /** Whatever is provided here will be set as the `src` attribute */
//    icon: string

//    text: string
//    textColor: string

//    /** CSS string */
//    background: string
//}

///** Defines a supported chain */
public interface IChain
{
    /** The chainId for the chain */
    [JsonProperty("")]
    public string ChainId { get; set; }

    /** One or more rpcEndpoints associated with that chainId */
    [JsonProperty("rpcEndpoints")]
    public IRpcEndpoint[] RpcEndpoints { get; set; }
}

public interface IRpcEndpoint
{
    [JsonProperty("protocol")]
    public string Protocol { get; set; }
    
    [JsonProperty("host")]
    public string Host { get; set; }
    
    [JsonProperty("port")]
    public ushort Port { get; set; }

    [JsonProperty("path")]
    public string? Path { get; set; } // nullable
}

///** Optional arguments to signTransaction */
public interface ISignTransactionConfig
{
    /** If the transaction should also be broadcast */
    public bool? broadcast { get; set; }

    /** Number of blocks behind (for use with eosjs) */
    public uint? blocksBehind { get; set; }

    /** Number of seconds before expiration (for use with eosjs) */
    public uint? expireSeconds { get; set; }
}

///** The object returned from signTransaction */
public interface ISignTransactionResponse
{
    /** Was the transaction broadcast */
    public bool wasBroadcast { get; set; }

    /** The transcation id (optional) */
    public string? transactionId { get; set; }

    /** The status of the transaction as returned by the RPC API (optional) */
    public string? status { get; set; }

    /** Set if there was an error */
    public IError Error { get; set; }

    public interface IError {

        /** The error code */
        public string code { get; set; }

        /** The error message */
        public string message { get; set; }

        /** The error name */
        public string name { get; set; }
    }
    
    /** The raw transaction object */
    public object transaction { get; set; }
}

//export const UALLoggedInAuthType = 'UALLoggedInAuthType'

//export const UALAccountName = 'UALAccountName'
