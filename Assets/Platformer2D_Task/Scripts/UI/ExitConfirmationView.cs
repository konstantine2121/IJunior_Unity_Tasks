namespace Platformer2D_Task.UI
{
    public class ExitConfirmationView : BaseMenuView, IExitConfirmationView
    {
        private const string ReturnName = "return-button";
        private const string ExitName = "exit-button";

        public MenuButton Exit { get; private set; }
        public MenuButton Return { get; private set; }

        public override MenuType MenuType => MenuType.ExitConfirmation;

        private void Awake()
        {
            Exit = new MenuButton(RootElement, ExitName);
            Return = new MenuButton(RootElement, ReturnName);
        }
    }
}
