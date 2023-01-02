namespace Platformer2D_Task.UI
{
    public interface IMainMenuView : IBaseMenuView
    {
        MenuButton Play { get; }
        
        MenuButton About { get; }
                
        MenuButton Exit { get; }
    }
}
