namespace Platformer2D_Task.UI
{
    public interface IMenuContainer
    {
        /// <summary>
        /// Главное меню
        /// </summary>
        IMainMenuView MainMenu { get; }

        /// <summary>
        /// Меню паузы
        /// </summary>
        IPauseMenuView PauseMenu { get; }

        /// <summary>
        /// Окно GameOver
        /// </summary>
        IGameOverView GameOver { get; }

        /// <summary>
        /// Окно подтверждения выхода из игры
        /// </summary>
        IExitConfirmationView ExitConfirm { get; }

        /// <summary>
        /// Об авторах
        /// </summary>
        IAboutView About { get; }

        /// <summary>
        /// Игровой экран
        /// </summary>
        IGameView GameView { get; }

        /// <summary>
        /// Текущее окно
        /// </summary>
        IBaseMenuView ActiveScreen { get; }

        IBaseMenuView GetScreen(MenuType menuType);

        /// <summary>
        /// Отобразить выбранное меню.<br/>
        /// Остальные окна при этом скроются.
        /// </summary>
        /// <param name="menuType"></param>
        void ShowScreen(MenuType menuType);

        void HideAllScreens();
    }
}
