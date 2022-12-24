using System;
using UnityEngine;

namespace UniversalAuthenticatorLibrary
{
    [Serializable]
    ///** The fields that an Authenticator can style on their button */
    public class ButtonStyle
    {
        /** Whatever is provided here will be set as the `src` attribute */
        public Sprite Icon;

        public string Text;
        public Color TextColor;
        public Color Background;
    }
}