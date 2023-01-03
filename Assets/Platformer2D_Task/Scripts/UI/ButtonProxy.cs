using System;
using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    public class ButtonProxy
    {
        public const string ButtonTag = "menu-button";

        public event Action Clicked;

        private readonly string _containerTemplateName;
        private readonly UIDocument _uiDocument;
        
        private Button _button;

        public Button Button => _button;
        
        public ButtonProxy(UIDocument uiDocument, string containerTemplateName)
        {
            if (uiDocument == null)
            {
                throw new ArgumentNullException(nameof(uiDocument));
            }

            if (string.IsNullOrEmpty(containerTemplateName))
            {
                throw new ArgumentException(nameof(containerTemplateName));
            }

            _uiDocument = uiDocument;
            _containerTemplateName = containerTemplateName;
        }

        public void OnEnableChanged(bool enable)
        {
            if (enable)
            {
                _button = FindButton(_containerTemplateName);
                _button.clicked += RaiseClicked;
            }
            else
            {
                if (_button != null)
                {
                    _button.clicked -= RaiseClicked;
                    _button = null;
                }
            }
        }

        protected Button FindButton(string templateContainerName)
        {
            if (string.IsNullOrEmpty(templateContainerName))
            {
                throw new ArgumentException(nameof(templateContainerName));
            }

            return _uiDocument.rootVisualElement
                .Q<TemplateContainer>(templateContainerName)
                .Q<Button>(ButtonTag);
        }

        private void RaiseClicked()
        {
            Clicked?.Invoke();
        }
    }
}
