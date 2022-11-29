using Assets.Packages.AnchorLinkTransportSharp.Src.Transports.UiToolkit.Ui;
using UnityEngine.UIElements;

namespace Assets.Packages.UniversalAuthenticatorLibrarySharp.Src.UiToolkit.Ui
{
    public class AuthenticatorsPanel : ScreenBase
    {
        private VisualElement _authenticatorButtonBox;

        /*
         * Child-Controls
         */
        private Button _closeViewButton;

        private Label _infoLabel;
        private Label _learnLabel;

        private VisualElement _learnMoreContainer;
        private VisualElement _infoContainer;

        private void Start()
        {
            _closeViewButton = Root.Q<Button>("close-view-button");
            _authenticatorButtonBox = Root.Q<VisualElement>("authenticator-button-box");
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

            _infoLabel.RegisterCallback<ClickEvent>(evt =>
            {
                _learnMoreContainer.Show();
                _infoContainer.Hide();
            });

            _learnLabel.RegisterCallback<ClickEvent>(evt =>
            {
                _learnMoreContainer.Hide();
                _infoContainer.Show();
            });
        }

        #endregion
    }
}