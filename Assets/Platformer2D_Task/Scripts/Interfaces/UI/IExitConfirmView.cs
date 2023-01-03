using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    public interface IExitConfirmView : IBaseMenuView
    {
        Button Exit { get; }

        Button Stay { get; }
    }
}
