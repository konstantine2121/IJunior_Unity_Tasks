using UnityEngine;
using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    public class MainMenuView : BaseMenuView, IMainMenuView
    {
        private const string PlayName = "play-button";
        private const string AboutName = "about-button";
        private const string ExitName = "exit-button";
        
        #region IMainMenuView Implementation

        public MenuButton Play { get; private set; }
        
        public MenuButton About { get; private set; }
        
        public MenuButton Exit { get; private set; }

        #endregion IMainMenuView Implementation

        public override MenuType MenuType => MenuType.MainMenu;

        private void Awake()
        {
            Play  = new MenuButton(RootElement, PlayName);
            About = new MenuButton(RootElement, AboutName);
            Exit  = new MenuButton(RootElement, ExitName);
        }
    }   
}
