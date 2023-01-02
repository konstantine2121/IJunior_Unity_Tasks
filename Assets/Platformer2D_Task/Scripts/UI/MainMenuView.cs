using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    public class MainMenuView : BaseMenuView, IMainMenuView
    {
        
        private const string PlayName = "play-button";
        private const string AboutName = "about-button";
        private const string ExitName = "exit-button";

        public MenuButton Play { get; private set; }
        
        public MenuButton About { get; private set; }
        
        public MenuButton Exit { get; private set; }

        public override MenuType MenuType => MenuType.MainMenu;

        private void Awake()
        {
            base.Awake();
            GetButtons();
        }

        private void GetButtons()
        {
            Play  = CreateButton(PlayName);
            About = CreateButton(AboutName);
            Exit  = CreateButton(ExitName);
        }
    }   
}
