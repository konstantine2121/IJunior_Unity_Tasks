namespace Platformer2D_Task.UI
{
    public class PauseMenuView : BaseMenuView, IPauseMenuView
    {
        private const string ResumeName = "resume-button";
        private const string MenuName = "menu-button";

        #region IPauseMenuView Implementation

        public MenuButton Resume { get; private set; }

        public MenuButton MainMenu { get; private set; }

        #endregion IPauseMenuView Implementation

        public override MenuType MenuType => MenuType.Pause;

        private void Awake()
        {
            Resume = new MenuButton(RootElement, ResumeName);
            MainMenu = new MenuButton(RootElement, MenuName);
        }
    }   
}
