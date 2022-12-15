using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UniversalAuthenticatorLibrary.Src.UiToolkit.Ui
{
    [Serializable()]
    public class BasicControl : MonoBehaviour
    {
        /// <summary>
        /// The UXML file to render
        /// </summary>
        public VisualTreeAsset uxml;

        /// <summary>
        /// The main style sheet file to give styles to Unity provided elements
        /// </summary>
        public StyleSheet unityStyleSheet;

        public VisualElement Clone()
        {
            var element = new VisualElement();

            uxml.CloneTree(element);
            element.styleSheets.Add(unityStyleSheet);

            return element;
        }
    }
}
