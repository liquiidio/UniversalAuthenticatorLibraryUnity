using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UniversalAuthenticatorLibrary.Src.UiToolkit.Ui
{
    [Serializable()]
    public class AuthenticatorButtonItem : BasicControl
    {
        public VisualElement Clone(ButtonStyle buttonStyle, Action onClickAction)
        {
            var element = new VisualElement();

            uxml.CloneTree(element);
            element.styleSheets.Add(unityStyleSheet);

            var logo = element.Q<VisualElement>("logo-icon");
            logo.style.backgroundImage = buttonStyle.Icon.texture;

            var walletTypeLabel = element.Q<Label>("authenticator-text");
            walletTypeLabel.text = buttonStyle.Text;
            walletTypeLabel.style.color = buttonStyle.TextColor;

            var walletBoxBackground = element.Q<VisualElement>("authenticator-box");
            walletBoxBackground.style.backgroundColor = buttonStyle.Background;

            element.RegisterCallback<ClickEvent>((clickEvent) => onClickAction());

            return element;
        }
    }
}
