using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using EosSharp.Core.Api.v1;

namespace UniversalAuthenticatorLibrary
{
    public class AuthenticatorResponse
    {
        /** List of authenticators */
        public Authenticator[] AvailableAuthenticators { get; set; }

        /** The Authenticator to use for auto login, if supported and available */
        public Authenticator AutoLoginAuthenticator { get; set; }
    }

    [Serializable]
    public class UALOptions
    {
        /** The Apps Identifier **/
        [JsonProperty("identifier")] public string Identifier;

        [JsonProperty("signTransactionConfig")]
        public SignTransactionConfig SignTransactionConfig;
    }

    ///** Defines a supported chain */
    [Serializable]
    public class Chain
    {
        /** The chainId for the chain */
        [JsonProperty("chainId")] public string ChainId;

        /** One or more rpcEndpoints associated with that chainId */
        [JsonProperty("rpcEndpoints")] public List<RpcEndpoint> RpcEndpoints;
    }

    [Serializable]
    public class RpcEndpoint
    {
        [JsonProperty("protocol")] public string Protocol;

        [JsonProperty("host")] public string Host;

        [JsonProperty("port")] public ushort Port;

        [JsonProperty("path")] public string Path;

        public string HttpEndpoint
            =>
                $"{(!string.IsNullOrEmpty(Protocol) ? $"{Protocol}://" : "")}{Host}{(Port != 0 ? $":{Port}" : "")}{(!string.IsNullOrEmpty(Path) ? $"/{Path}" : "")}";

    }

    ///** Optional arguments to signTransaction */
    [Serializable]
    public class SignTransactionConfig
    {
        /** If the transaction should also be broadcast */
        [JsonProperty("broadcast")] public bool Broadcast;

        /** Number of blocks behind */
        [JsonProperty("blocksBehind")] public uint BlocksBehind;

        /** Number of seconds before expiration */
        [JsonProperty("expireSeconds")] public uint ExpireSeconds;
    }

    public class UalError
    {
        /** The error code */
        [JsonProperty("code")]
        public string Code { get; set; }

        /** The error message */
        [JsonProperty("message")]
        public string Message { get; set; }

        /** The error name */
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    ///** The object returned from signTransaction */
    public class SignTransactionResponse
    {
        /** The status of the transaction as returned by the RPC API (optional) */
        [JsonProperty("status")]
        public string? Status { get; set; }

        /** Set if there was an error */
        [JsonProperty("ualError")]
        public UalError UalError { get; set; }

        /** The raw transaction object */
        [JsonProperty("transaction")]
        public ProcessedTransaction Transaction { get; set; }
    }
}