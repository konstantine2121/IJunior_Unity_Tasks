using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class BaseMenuView: MonoBehaviour, IBaseMenuView
    {
        protected UIDocument UIDocument;

        public event Action<bool> EnabledChanged;

        public abstract MenuType MenuType { get; }

        public bool Visible => UIDocument != null ? UIDocument.enabled : false;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        protected void Awake()
        {
            UIDocument = GetComponent<UIDocument>();
        }

        protected void RaiseEnabledChanged(bool enable)
        {
            EnabledChanged?.Invoke(enable);
        }
    }
}
