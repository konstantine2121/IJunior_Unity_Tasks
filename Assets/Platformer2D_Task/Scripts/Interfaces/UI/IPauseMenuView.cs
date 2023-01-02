namespace Platformer2D_Task.UI
{
    public interface IPauseMenuView : IBaseMenuView
    {
        MenuButton Resume { get; }

        MenuButton MainMenu { get; }
    }
}
