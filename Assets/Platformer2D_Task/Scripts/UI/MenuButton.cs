using System;
using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    public class MenuButton
    {
        private const string ButtonTag = "menu-button";

        public event Action Clicked;

        private readonly VisualElement _rootVisualElement;
        
        private Button _button;
        
        public MenuButton(VisualElement rootVisualElement, string containerTemplateName)
        {
            if (rootVisualElement == null)
            {
                throw new ArgumentNullException(nameof(rootVisualElement));
            }

            if (string.IsNullOrEmpty(containerTemplateName))
            {
                throw new ArgumentException(nameof(containerTemplateName));
            }

            _rootVisualElement = rootVisualElement;
            _button = FindButton(containerTemplateName);            
            _button.clicked += RaiseClicked;
        }

        ~MenuButton()
        {
            _button.clicked -= RaiseClicked;
        }

        public Button Button => _button;

        protected Button FindButton(string templateContainerName)
        {
            if (string.IsNullOrEmpty(templateContainerName))
            {
                throw new ArgumentException(nameof(templateContainerName));
            }

            return _rootVisualElement
                .Q<TemplateContainer>(templateContainerName)
                .Q<Button>(ButtonTag);
        }

        private void RaiseClicked()
        {
            Clicked?.Invoke();
        }
    }
}
