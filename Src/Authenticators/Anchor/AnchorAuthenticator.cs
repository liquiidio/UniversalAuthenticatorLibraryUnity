using AnchorLinkSharp;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EosioSigningRequest;
using EosSharp.Core.Api.v1;
using UnityEngine;

public class AnchorAuthenticator : Authenticator
{
    private readonly AnchorLink _link;
    private string _identifier;

    private readonly List<AnchorUser> _users = new List<AnchorUser>();

    public AnchorAuthenticator(IChain[] chains, object options) : base(chains, options)
    {
        _link = new AnchorLink(new LinkOptions()
        {
            ChainId = "",
            Rpc = "",
            Service = "",
        });
    }

    public override async Task<User[]> Login(string accountName = null)
    {
        var session = await _link.RestoreSession(_identifier, new PermissionLevel()
        {
            actor = accountName,
            permission = SigningRequestConstants.PlaceholderPermission
        });

        if (session == null)
        {
            var identifyResult = await _link.Login(_identifier);
            session = identifyResult.Session;
        }
        var newUsers = new AnchorUser[] {new AnchorUser(session)};
        _users.AddRange(newUsers.ToList());
        return newUsers;
    }

    public override async Task Logout()
    {
        foreach (var anchorUser in _users)
        {
            // TODO remove Session (this is from Storage) - or just clear the current Users and active Sessions?
            await anchorUser.Session.Remove();
        }
    }

    public override bool ShouldAutoLogin()
    {
        return true;
    }

    public override bool ShouldRender()
    {
        return true;
    }
}
