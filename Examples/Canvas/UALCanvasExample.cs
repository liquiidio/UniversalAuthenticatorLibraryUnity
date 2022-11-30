using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UALCanvasExample : MonoBehaviour
{
    public UnityCanvasUAL UnityCanvasUAL;

    // Start is called before the first frame update
    void Start()
    {
        UnityCanvasUAL.OnUserLogin += UserLogin;
        UnityCanvasUAL.Init();
    }

    private void UserLogin(User _user)
    {
        // This function can sign transactions using _user.Sign...
    }
}
