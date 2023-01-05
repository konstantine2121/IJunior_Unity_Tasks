using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    public interface IExitConfirmationView : IBaseMenuView
    {
        MenuButton Exit { get; }

        MenuButton Return { get; }
    }
}
