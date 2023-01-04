using System;

namespace Platformer2D_Task.UI
{
    public interface IBaseMenuView
    {
        MenuType MenuType { get; }

        bool Visible { get; }

        void Show();

        void Hide();
    }
}
