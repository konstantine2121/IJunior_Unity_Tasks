namespace Platformer2D_Task.UI
{
    public class AboutMenuView : BaseMenuView, IAboutView
    {
        private const string BackName = "back-button";

        public MenuButton MainMenu { get; private set; }

        public override MenuType MenuType => MenuType.About;

        private void Awake()
        {
            MainMenu = new MenuButton(RootElement, BackName);
        }
    }
}
