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

        public ButtonProxy Play { get; private set; }
        
        public ButtonProxy About { get; private set; }
        
        public ButtonProxy Exit { get; private set; }

        #endregion IMainMenuView Implementation

        public override MenuType MenuType => MenuType.MainMenu;

        private new void Awake()
        {
            base.Awake();

            Play = new ButtonProxy(UIDocument, PlayName);
            About = new ButtonProxy(UIDocument, AboutName);
            Exit = new ButtonProxy(UIDocument, ExitName);

            EnabledChanged += Play.OnEnableChanged;
            EnabledChanged += About.OnEnableChanged;
            EnabledChanged += Exit.OnEnableChanged;
        }

        private void OnEnable()
        {
            RaiseEnabledChanged(true);
        }

        private void OnDisable()
        {
            RaiseEnabledChanged(false);
        }
    }   
}
