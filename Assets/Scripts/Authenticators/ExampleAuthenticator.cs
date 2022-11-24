using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExampleAuthenticator : Authenticator
{
    public ExampleAuthenticator(IChain[] chains, object options) : base(chains, options)
    {

    }

    public override Task<User[]> Login(string accountName = null)
    {
        return null;
    }

    public override Task Logout()
    {
        return null;
    }

    public override bool ShouldAutoLogin()
    {
        return false;
    }

    public override bool ShouldRender()
    {
        return true;
    }
}
