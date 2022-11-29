using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityUiToolkitUAL : UnityUAL
{
    public UnityUiToolkitUAL(Chain chain, UALOptions ualOptions, List<Authenticator> authenticators) : base(chain, ualOptions, authenticators)
    {
    }

    protected override void CreateUalPanel(Authenticator[] authenticators)
    {
        //foreach (var authenticator in authenticators)
        //{
        //    // Has Icon, Style, TextColor etc.
        //    var buttonStyle = authenticator.GetStyle();

        //    // called when the specific Button is pressed
        //    Action onClick = () => LoginUser(authenticator);
        //}
    }
}
