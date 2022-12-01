using System.Collections;
using System.Collections.Generic;
using Assets.Packages.UniversalAuthenticatorLibrary.Examples.UiToolkit.Ui;
using Assets.Packages.UniversalAuthenticatorLibrary.Src.UiToolkit;
using UnityEngine;

namespace Assets.Packages.UniversalAuthenticatorLibrary.Examples.UiToolkit
{
    public class UALUiToolkitExample : MonoBehaviour
    {
        public UnityUiToolkitUAL UnityUiToolkitUal;
        public ExampleMainView ExampleMainView;

        // Start is called before the first frame update
        void Start()
        {
            UnityUiToolkitUal.OnUserLogin += UserLogin;
            UnityUiToolkitUal.Init();
        }

        private void UserLogin(User _user)
        {
            // This function can sign transactions using _user.Sign...
        }
    }

}