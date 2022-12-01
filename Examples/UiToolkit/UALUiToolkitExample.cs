using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UALUiToolkitExample : MonoBehaviour
{
    public UnityUiToolkitUAL UnityUiToolkitUal;

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