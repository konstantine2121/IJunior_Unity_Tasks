using UnityEngine;
using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class BaseMenuView: MonoBehaviour, IBaseMenuView
    {
        protected UIDocument UIDocument;

        public abstract MenuType MenuType { get; }

        public bool Visible => UIDocument != null ? UIDocument.enabled : false;


        public void Show()
        {
            UIDocument.enabled = true;
        }

        public void Hide()
        {
            UIDocument.enabled = false;
        }

        protected void Awake()
        {
            UIDocument = GetComponent<UIDocument>();
        }

        protected MenuButton CreateButton(string templateContainerName)
        {
            return MenuButton.CreateButton(UIDocument, templateContainerName);
        }
    }
}
