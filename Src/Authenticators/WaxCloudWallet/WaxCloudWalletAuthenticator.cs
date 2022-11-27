using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class WaxCloudWalletAuthenticator : Authenticator
{
    private readonly WaxCloudWalletPlugin _waxCloudWalletPlugin;

    private WaxCloudWalletUser _user;

    public WaxCloudWalletAuthenticator(IChain[] chains, object options) : base(chains, options)
    {
        _waxCloudWalletPlugin = new GameObject(nameof(WaxCloudWalletPlugin)).AddComponent<WaxCloudWalletPlugin>();
        _waxCloudWalletPlugin.OnLoggedIn = loginEvent =>
        {
            _user = new WaxCloudWalletUser(loginEvent.Account, _waxCloudWalletPlugin);
        };
    }

    public override async Task<User[]> Login(string accountName = null)
    {
        _waxCloudWalletPlugin.Login();

        int i = 0;
        while (_user == null && i < 200)
        {
            await UniTask.Delay(100);
            i++;
        }

        return new[] {_user};
    }

    public override async Task Logout()
    {
        // TODO reset WcwPlugin
        _user = null;
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
