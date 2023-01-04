using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class BaseMenuView: MonoBehaviour, IBaseMenuView
    {
        protected VisualElement _rootElement;

        protected VisualElement RootElement 
        { 
            get
            {
                if (_rootElement == null)
                {
                    _rootElement = GetComponent<UIDocument>().rootVisualElement;
                }

                return _rootElement;
            }
        }

        public abstract MenuType MenuType { get; }

        public bool Visible => RootElement.style.display == DisplayStyle.Flex;

        public void Show()
        {
            RootElement.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            RootElement.style.display = DisplayStyle.None;
        }
    }
}
