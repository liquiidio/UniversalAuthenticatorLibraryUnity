using EosSharp.Core.Api.v1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiUiExampleScript : MonoBehaviour
{
    private UnityUAL _unityUal;

    public UnityCanvasUAL UnityCanvasUAL;
    public UnityUiToolkitUAL UnityUiToolkitUAL;

    public bool UseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if (UseCanvas)
            _unityUal = UnityCanvasUAL;
        else
            _unityUal = UnityUiToolkitUAL;

        _unityUal.OnUserLogin += OnLoggedIn;
        _unityUal.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLoggedIn(User[] users)
    {
        var user = users[0];

        user.SignTransaction(new Action[] {
            new Action()
                {
                    account = "eosio.token",
                    name = "transfer",
                    authorization = new List<PermissionLevel>() {  }, // TODO
                    data = new Dictionary<string, object>()
                    {
                        { "from", user },
                        { "to", "teamgreymass" },
                        { "quantity", "0.0001 EOS" },
                        { "memo", "Anchor is the best! Thank you <3" }
                    }
                }
        });
    }

}
