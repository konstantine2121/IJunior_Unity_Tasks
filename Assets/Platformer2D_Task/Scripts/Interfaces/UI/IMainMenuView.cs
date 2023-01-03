namespace Platformer2D_Task.UI
{
    public interface IMainMenuView : IBaseMenuView
    {
        ButtonProxy Play { get; }

        ButtonProxy About { get; }

        ButtonProxy Exit { get; }
    }
}
