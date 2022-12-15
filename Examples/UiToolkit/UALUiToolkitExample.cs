using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using eossharp.EosSharp.EosSharp.Unity3D;
using UniversalAuthenticatorLibrary.Examples.UiToolkit.Ui;
using UniversalAuthenticatorLibrary.Src.UiToolkit;
using EosSharp.Core.Api.v1;
using UnityEngine;

namespace UniversalAuthenticatorLibrary.Examples.UiToolkit
{
    public class UALUiToolkitExample : MonoBehaviour
    {
        public User User;
        public UnityUiToolkitUAL UnityUiToolkitUal;

        async void Start()
        {
            await UnityUiToolkitUal.Init();
            UnityUiToolkitUal.OnUserLogin += UserLogin;
        }

        private async void UserLogin(User user)
        {
            User = user;
            Debug.Log($"User with account-name {await user.GetAccountName()} logged in with {user.GetWalletType()}");
        }

        // transfer tokens using a session  
        public async Task Transact(EosSharp.Core.Api.v1.Action action)
        {
            var transactResult = await User.SignTransaction(new []{ action });
        }

    }

}