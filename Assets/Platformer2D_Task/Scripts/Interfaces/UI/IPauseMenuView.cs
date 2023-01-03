using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    public interface IPauseMenuView : IBaseMenuView
    {
        Button Resume { get; }

        Button MainMenu { get; }
    }
}
