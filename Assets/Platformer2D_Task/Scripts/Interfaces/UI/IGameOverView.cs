using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    public interface IGameOverView : IBaseMenuView
    {
        Button Restart { get; }

        Button MainMenu { get; }
    }
}
