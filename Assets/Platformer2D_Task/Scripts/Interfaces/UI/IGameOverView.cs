namespace Platformer2D_Task.UI
{
    public interface IGameOverView : IBaseMenuView, IScoreBar
    {
        MenuButton Restart { get; }

        MenuButton MainMenu { get; }
    }
}
