using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Packages.UniversalAuthenticatorLibrary.Examples.UiToolkit.Ui;
using Assets.Packages.UniversalAuthenticatorLibrary.Src.UiToolkit;
using EosSharp.Core.Api.v1;
using UnityEngine;

namespace Assets.Packages.UniversalAuthenticatorLibrary.Examples.UiToolkit
{
    public class UALUiToolkitExample : MonoBehaviour
    {
        public User User;
        public UnityUiToolkitUAL UnityUiToolkitUal;

        // Start is called before the first frame update
        void Start()
        {
            UnityUiToolkitUal.Init();
            UnityUiToolkitUal.OnUserLogin += UserLogin;
        }

        void UserLogin(User user)
        {
            User = user;
        }

        // transfer tokens using a session  
        public async Task Transact(EosSharp.Core.Api.v1.Action action)
        {
            var transactResult = await User.SignTransaction(new []{ action });
        }

    }

}