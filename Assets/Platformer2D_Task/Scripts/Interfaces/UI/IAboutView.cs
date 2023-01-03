using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    public interface IAboutView: IBaseMenuView
    {
        Button MainMenu { get; }
    }
}
