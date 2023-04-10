using System.Threading.Tasks;
using UniversalAuthenticatorLibrary.Src.UiToolkit;
using UniversalAuthenticatorLibrary.Examples.UiToolkit.Ui;
using UnityEngine;

namespace UniversalAuthenticatorLibrary.Examples.UiToolkit
{
    public class UALUiToolkitExample : MonoBehaviour
    {
        public User User;
        public UnityUiToolkitUAL UnityUiToolkitUal;
        public UALExamplePanel UalExamplePanel;

        async void Start()
        {
            await UnityUiToolkitUal.Init();
            UnityUiToolkitUal.OnUserLogin += OnShowMainView;
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

        private void OnShowMainView(User user)
        {
            UalExamplePanel.Rebind(user);
        }
    }

}