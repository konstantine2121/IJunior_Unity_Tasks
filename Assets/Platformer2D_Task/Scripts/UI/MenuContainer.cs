using UnityEngine;
using System.Linq;

namespace Platformer2D_Task.UI
{
    public class MenuContainer : MonoBehaviour, IMenuContainer
    {
        private IBaseMenuView[] _menuViews;

        private IBaseMenuView _activeScreen;

        #region IMenuContainer Implementaion

        public IMainMenuView MainMenu { get; private set; }

        public IPauseMenuView PauseMenu { get; private set; }

        public IGameOverView GameOver { get; private set; }
        
        public IExitConfirmationView ExitConfirm { get; private set; }
        
        public IAboutView About { get; private set; }
        
        public IGameView GameView { get; private set; }
        
        public IBaseMenuView ActiveScreen 
        {
            get 
            {
                if (_activeScreen != null)
                {
                    return _activeScreen;
                }

                var visibleMenu = _menuViews.FirstOrDefault(view => view.Visible);
                if (visibleMenu != null)
                {
                    _activeScreen = visibleMenu;
                    return visibleMenu;
                }

                return null;
            }
            private set
            {
                _activeScreen = value;
            }
        }
        
        public IBaseMenuView GetScreen(MenuType menuType)
        {
            return _menuViews.FirstOrDefault(view => view.MenuType == menuType);
        }

        public void ShowScreen(MenuType menuType)
        {
            var view = GetScreen(menuType);
            if (view == null)
            {
                return;
            }

            ActiveScreen?.Hide();
            view.Show();
            ActiveScreen = view;
        }

        public void HideAllScreens()
        {
            foreach (var view in _menuViews)
            {
                view?.Hide();
            }
        }

        #endregion IMenuContainer Implementaion

        private void Awake()
        {
            GetMenus();
        }

        private void GetMenus()
        {
            _menuViews = GetComponentsInChildren<IBaseMenuView>();
            
            MainMenu = GetMenu<IMainMenuView>();
            PauseMenu = GetMenu<IPauseMenuView>();
            GameOver = GetMenu<IGameOverView>();
            ExitConfirm = GetMenu<IExitConfirmationView>();
            About = GetMenu<IAboutView>();
            GameView = GetMenu<IGameView>();
        }

        private T GetMenu<T>() 
            where T : class, IBaseMenuView
        {
            return _menuViews.First(view => view is T) as T;
        }
    }
}
