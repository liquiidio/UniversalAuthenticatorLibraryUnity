using UnityEngine;
using UnityEngine.UIElements;

namespace UniversalAuthenticatorLibrary.Src.UiToolkit.Ui
{
    [RequireComponent(typeof(UIDocument))]
    public class ScreenBase : MonoBehaviour
    {
        public VisualElement Root;

        public UIDocument Screen;

        private void Awake()
        {
            Screen = GetComponent<UIDocument>();
            Root = Screen.rootVisualElement;

            Hide();
        }

        /// <summary>
        /// Show this Screen (set it to visible)
        /// </summary>
        public void Show()
        {
            Root.Show();
        }

        /// <summary>
        /// Hide this Screen (set it to invisible)
        /// </summary>
        /// <param name="element"></param>
        public void Hide()
        {
            Root.Hide();
        }
    }
}