using System;
using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    public class MenuButton : BaseButton
    {
        public const string ButtonTag = "menu-button";

        private Button _button;

        public static MenuButton CreateButton(UIDocument uiDocument, string templateContainerName)
        {
            return new MenuButton(FindButton(uiDocument, templateContainerName), string.Empty);
        }

        private static Button FindButton(UIDocument uiDocument, string templateContainerName)
        {
            if (uiDocument == null)
            {
                throw new ArgumentNullException(nameof(uiDocument));
            }

            if (string.IsNullOrEmpty(templateContainerName))
            {
                throw new ArgumentException(nameof(templateContainerName));
            }

            return uiDocument.rootVisualElement
                .Q<TemplateContainer>(templateContainerName)
                .Q<Button>(ButtonTag);
        }

        public MenuButton(Button button, string text = "")
        {
            if (button == null)
            {
                throw new ArgumentNullException();
            }

            _button = button;

            if (!string.IsNullOrEmpty(text))
            {
                Text = text;
            }

            _button.clicked += Button_Clicked;
        }

        ~MenuButton()
        {
            _button.clicked -= Button_Clicked;
        }

        public override string Text
        {
            get
            {
                return _button.text;
            }
            set
            {
                _button.text = value;
            }
        }

        private void Button_Clicked()
        {
            Clicked?.Invoke(this);
        }
    }
}
