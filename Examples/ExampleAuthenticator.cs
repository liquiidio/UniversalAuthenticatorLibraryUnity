using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExampleAuthenticator : Authenticator
{
    public ExampleAuthenticator(Chain chain, UALOptions options) : base(chain, options)
    {

    }

    public override void Init(Chain chain, UALOptions options)
    {

    }

    public override Task<User> Login(string accountName = null)
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
