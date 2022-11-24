using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
///** The fields that an Authenticator can style on their button */
public class ButtonStyle
{
    /** Whatever is provided here will be set as the `src` attribute */
    public Texture2D Icon;
    public string Text;
    public Color32 TextColor;
    public Color32 Background;
}
