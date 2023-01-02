namespace Platformer2D_Task.UI
{
    public interface IGameOverView : IBaseMenuView
    {
        MenuButton Restart { get; }

        MenuButton MainMenu { get; }
    }
}
