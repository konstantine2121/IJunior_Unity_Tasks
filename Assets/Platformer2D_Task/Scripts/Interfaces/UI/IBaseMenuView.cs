using System;

namespace Platformer2D_Task.UI
{
    public interface IBaseMenuView
    {
        event Action<bool> EnabledChanged;

        MenuType MenuType { get; }

        bool Visible { get; }

        void Show();

        void Hide();
    }
}
