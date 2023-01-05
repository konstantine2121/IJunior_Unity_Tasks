using System;
using System.Collections.Generic;
using Platformer2D_Task.UI;
using UnityEngine;

namespace Platformer2D_Task
{
    class MenuController : MonoBehaviour
    {
        [SerializeField]private MenuContainer _menuContainer;
        [SerializeField]private KeyCode PauseKey = KeyCode.Escape;

        public IGameController GameController;

        private readonly Dictionary<MenuButton, Action> _buttonsHandlers = new Dictionary<MenuButton, Action>();

        private bool OnPause => _menuContainer.GetScreen(MenuType.Pause).Visible;

        private bool InGame => _menuContainer.GetScreen(MenuType.Game).Visible;

        /// <summary>
        /// Создаем новый обет меню контроллера.
        /// </summary>
        /// <param name="menuContainer">Контейнер со списком менюшек</param>
        /// <param name="restarter">Загрузчик сцены</param>
        /// <exception cref="ArgumentNullException"></exception>
        private void Start()
        {
            if (_menuContainer == null)
            {
                throw new NullReferenceException(nameof(_menuContainer));
            }

            _menuContainer.HideAllScreens();
            _menuContainer.ShowScreen(MenuType.MainMenu);

            InitializeHandlers();
            SubscribeHandlers();
        }

        private void Update()
        {
            if (Input.GetKeyDown(PauseKey)  && (InGame | OnPause))
            {
                if (InGame)
                {
                    OnPauseClicked();
                }
                else                
                {
                    OnResumeClicked();
                }
            }
        }

        private void OnDestroy()
        {
            UnsubscribeHandlers();
        }

        private void InitializeHandlers()
        {
            var dic = _buttonsHandlers;

            if (_menuContainer == null)
            {
                return;
            }

            var main = _menuContainer.MainMenu;
            if (main != null)
            {
                dic.Add(main.Play, OnPlayClicked);
                dic.Add(main.About, OnAboutClicked);
                dic.Add(main.Exit, OnGoToExitClicked);
            }

            var about = _menuContainer.About;
            if (about != null)
            {
                dic.Add(about.MainMenu, OnMainMenuClicked);
            }

            var pause = _menuContainer.PauseMenu;
            if (pause != null)
            {
                dic.Add(pause.MainMenu, OnMainMenuClicked);
                dic.Add(pause.Resume, OnResumeClicked);
            }

            var gameover = _menuContainer.GameOver;
            if (gameover != null)
            {
                dic.Add(gameover.MainMenu, OnMainMenuClicked);
                dic.Add(gameover.Restart, OnPlayClicked);
            }

            var exit = _menuContainer.ExitConfirm;
            if (exit != null)
            {
                dic.Add(exit.Return, OnMainMenuClicked);
                dic.Add(exit.Exit, OnExitClicked);
            }
        }

        private void OnExitClicked()
        {
            Application.Quit();
        }

        private void OnGoToExitClicked()
        {
            _menuContainer.ShowScreen(MenuType.ExitConfirmation);
        }

        private void OnResumeClicked()
        {
            GameController.Resume();
            _menuContainer.ShowScreen(MenuType.Game);
        }

        private void OnPauseClicked()
        {
            GameController?.Pause();
            _menuContainer.ShowScreen(MenuType.Pause);
        }

        private void SubscribeHandlers()
        {
            foreach (var (button, handler) in _buttonsHandlers)
            {
                button.Clicked += handler;
            }
        }

        private void UnsubscribeHandlers()
        {
            foreach (var (button, handler) in _buttonsHandlers)
            {
                button.Clicked -= handler;
            }
        }

        private void OnPlayClicked()
        {
            _menuContainer.ShowScreen(MenuType.Game);
            GameController?.Restart();
        }

        private void OnAboutClicked()
        {
            _menuContainer.ShowScreen(MenuType.About);
        }

        private void OnMainMenuClicked()
        {
            GameController.Stop();
            _menuContainer.ShowScreen(MenuType.MainMenu);
        }
    }
}
