using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using eossharp.EosSharp.EosSharp.Unity3D;
using UnityEngine;

public class WombatAuthenticator : Authenticator
{
#if UNITY_WEBGL
    private WombatPlugin _wombatPlugin;

    private WombatUser _user;

    public WombatAuthenticator(Chain chain, UALOptions options) : base(chain, options)
    {
        Init(chain, options);
    }

    public sealed override void Init(Chain chain, UALOptions options)
    {
        _wombatPlugin = new GameObject(nameof(WombatPlugin)).AddComponent<WombatPlugin>();
        _wombatPlugin.OnLoggedIn = loginEvent =>
        {
            _user = new WombatUser(loginEvent.Account, _wombatPlugin);
        };
    }

    public override async Task<User> Login(string accountName = null)
    {
        _wombatPlugin.Login();

        int i = 0;
        while (_user == null && i < 200)
        {
            await AsyncHelper.Delay(100);
            i++;
        }

        return _user;
    }

    public override async Task Logout()
    {
        // TODO reset WombatPlugin
        _user = null;
    }

    public override bool ShouldAutoLogin()
    {
        return false;
    }

    public override bool ShouldRender()
    {
        return true;
    }

#elif UNITY_EDITOR

    public WombatAuthenticator(Chain chain, UALOptions options) : base(chain, options)
    {
        Init(chain, options);
    }

    public sealed override void Init(Chain chain, UALOptions options)
    {
    }

    public override async Task<User> Login(string accountName = null)
    {
        Debug.Log("Wombat-Wallet can't be used on platforms other than WebGL, returning unusable Fake-User");
        return new WombatUser("fake.user", null);
    }

    public override async Task Logout()
    {
        return;
    }

    public override bool ShouldAutoLogin()
    {
        return false;
    }

    public override bool ShouldRender()
    {
        return true;
    }
#else
    public WombatAuthenticator(Chain chain, UALOptions options) : base(chain, options)
    {
        Init(chain, options);
    }

    public sealed override void Init(Chain chain, UALOptions options)
    {
        Debug.Log("Wombat-Wallet can't be used on platforms other than WebGL");
    }

    public override async Task<User> Login(string accountName = null)
    {
        return null;
    }

    public override async Task Logout()
    {
        return;
    }

    public override bool ShouldAutoLogin()
    {
        return false;
    }

    public override bool ShouldRender()
    {
        return false;
    }
#endif

}
