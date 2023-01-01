namespace Platformer2D_Task
{
    public interface ICommonMenuView
    {
        MenuType MenuType { get; }

        void Show();

        void Hide();
    }
}
