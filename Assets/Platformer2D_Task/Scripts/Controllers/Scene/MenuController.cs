using System;
using System.Collections.Generic;
using Platformer2D_Task.UI;
using UnityEngine;

namespace Platformer2D_Task
{
    class MenuController : MonoBehaviour
    {
        [SerializeField]private MenuContainer _menuContainer;

        public IGameRestarter Restarter;

        private readonly Dictionary<MenuButton, Action> _buttonsHandlers = new Dictionary<MenuButton, Action>();

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

            SubscribeHandlers();
        }

        private void OnDestroy()
        {
            UnsubscribeHandlers();
        }

        private void SubscribeHandlers()
        {
            if (_menuContainer == null)
            {
                return;
            }

            var main = _menuContainer.MainMenu;
            if (main != null)
            {
                main.Play.Clicked += OnPlayClicked;
                main.About.Clicked += OnAboutClicked;
            }

            var about = _menuContainer.About;
            if (about != null)
            {
                about.MainMenu.Clicked += OnMainMenuClicked;
            }
        }

        private void UnsubscribeHandlers()
        {
            if (_menuContainer == null)
            {
                return;
            }

            var main = _menuContainer.MainMenu;
            if (main != null)
            {
                main.Play.Clicked -= OnPlayClicked;
            }
        }

        private void OnPlayClicked()
        {
            _menuContainer.ShowScreen(MenuType.Game);
            Restarter?.Restart();
        }

        private void OnAboutClicked()
        {
            _menuContainer.ShowScreen(MenuType.About);
        }

        private void OnMainMenuClicked()
        {
            _menuContainer.ShowScreen(MenuType.MainMenu);
        }
    }
}
