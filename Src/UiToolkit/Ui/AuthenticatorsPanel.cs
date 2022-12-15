using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UniversalAuthenticatorLibrary.Src.UiToolkit.Ui
{
    public class AuthenticatorsPanel : ScreenBase
    {

        /*
         * Child-Controls
         */
        private Button _closeViewButton;

        private VisualElement _learnMoreContainer;
        private VisualElement _infoContainer;
        private VisualElement _learnBox;
        private VisualElement _infoBox;

        public VisualElement AuthenticatorButtonBox;

        /*
         * Cloneable
         */
        [SerializeField] internal AuthenticatorButtonItem AuthenticatorButtonItem;
        void Start()
        {
            _closeViewButton = Root.Q<Button>("close-view-button");
            _learnBox = Root.Q<VisualElement>("learn-box");
            _infoBox = Root.Q<VisualElement>("button-container");
            _learnMoreContainer = Root.Q<VisualElement>("learn-more-container");
            _infoContainer = Root.Q<VisualElement>("info-container");

            AuthenticatorButtonBox = Root.Q<VisualElement>("authenticator-button-box");

            BindButtons();
        }

        #region Button Binding

        private void BindButtons()
        {
            _closeViewButton.clickable.clicked += Hide;

            _infoBox.RegisterCallback<ClickEvent>(evt =>
            {
                _learnMoreContainer.Show();
                _infoContainer.Hide();
            });

            _learnBox.RegisterCallback<ClickEvent>(evt =>
            {
                _learnMoreContainer.Hide();
                _infoContainer.Show();
            });
        }

        #endregion
    }
}