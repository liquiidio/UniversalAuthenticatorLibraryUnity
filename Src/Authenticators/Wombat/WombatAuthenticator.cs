using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class WombatAuthenticator : Authenticator
{
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
            await UniTask.Delay(100);
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
#if UNITY_WEBGL || UNITY_EDITOR
        return true;        
#endif
        return false;
    }
}
