using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Packages.UniversalAuthenticatorLibrarySharp.Src.UiToolkit.Ui;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Packages.UniversalAuthenticatorLibrarySharp.Src.UiToolkit.Ui
{
    public class AuthenticatorButtonItem : BasicControl
    {
        public VisualElement Clone(Texture2D icon, string authenticatorText, Color textColor, Color background, Action onClickAction)
        {
            var element = new VisualElement();

            uxml.CloneTree(element);
            element.styleSheets.Add(unityStyleSheet);

            var logo = element.Q<VisualElement>("logo-icon");
            logo.style.backgroundImage = icon;

            var walletTypeLabel = element.Q<Label>("authenticator-text");
            walletTypeLabel.text = authenticatorText;
            walletTypeLabel.style.color = textColor;

            var walletBoxBackground = element.Q<VisualElement>("authenticator-box");
            walletBoxBackground.style.backgroundColor = background;

            element.RegisterCallback<ClickEvent>((clickEvent) => onClickAction());

            return element;
        }
    }
}
