using Unity.VisualScripting;
using System;
using UnityEngine.UIElements;

namespace Assets.Packages.UniversalAuthenticatorLibrary.Src.UiToolkit.Ui
{
    public class AuthenticatorsPanel : ScreenBase
    {
        /*
         * Cloneable
         */
        public AuthenticatorButtonItem AuthenticatorButtonItem;

        /*
         * Child-Controls
         */
        private Button _closeViewButton;

        private Label _infoLabel;
        private Label _learnLabel;

        private VisualElement _learnMoreContainer;
        private VisualElement _infoContainer;
        public VisualElement _authenticatorButtonBox;
        private VisualElement _learnBox;
        private VisualElement _infoBox;

        private void Start()
        {
            _closeViewButton = Root.Q<Button>("close-view-button");
            _authenticatorButtonBox = Root.Q<VisualElement>("authenticator-button-box");
            _learnBox = Root.Q<VisualElement>("learn-box");
            _infoBox = Root.Q<VisualElement>("button-container");
            _learnMoreContainer = Root.Q<VisualElement>("learn-more-container");
            _infoContainer = Root.Q<VisualElement>("info-container");
            _infoLabel = Root.Q<Label>("info-label");
            _learnLabel = Root.Q<Label>("learn-label");

            BindButtons();
        }

        #region Button Binding

        private void BindButtons()
        {
            _closeViewButton.clickable.clicked += () => Hide();

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

        #region Rebind

        public void Rebind(Authenticator[] authenticators, Action<Authenticator> onClick)
        {
            _authenticatorButtonBox.Add(AuthenticatorButtonItem.Clone());

            foreach (var authenticator in authenticators)
            {
                // Has Icon, Style, TextColor etc.
                var buttonStyle = authenticator.GetStyle();

                _authenticatorButtonBox.Add(AuthenticatorButtonItem.Clone(buttonStyle, () =>
                { 
                    //LoginUser(authenticator);
                    Hide();
                }));
            }
        }
        #endregion

    }
}