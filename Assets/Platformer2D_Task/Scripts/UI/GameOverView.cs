using UnityEngine;
using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{    
    public class GameOverView : BaseMenuView , IGameOverView
    {
        private const string RestartName = "restart-button";
        private const string MainMenuName = "menu-button";
        private const string ScoreLabelName = "score-label";

        private BoxCollector _collector;
        private Label _scoreLabel;

        #region IGameOverView

        public MenuButton Restart { get; private set; }

        public MenuButton MainMenu { get; private set; }

        #endregion IGameOverView

        public override MenuType MenuType => MenuType.GameOver;

        public bool CollectorEnabled => _collector != null;

        public bool ScoreEnabled => _scoreLabel != null;

        #region IScoreBar

        public void RegisterCollector(BoxCollector collector)
        {
            UnregisterCollector();

            if (collector != null)
            {
                _collector = collector;
                _collector.NumberOfCoinsChanged += UpdateScore;

                UpdateScore(collector.GearBoxes);
            }
        }

        public void UnregisterCollector()
        {
            if (_collector != null)
            {
                _collector.NumberOfCoinsChanged -= UpdateScore;
            }
        }

        #endregion IScoreBar

        private void Awake()
        {
            _scoreLabel = RootElement.Q<Label>(ScoreLabelName);
            Restart = new MenuButton(RootElement, RestartName);
            MainMenu = new MenuButton(RootElement, MainMenuName);
        }

        private void OnEnable()
        {
            RegisterCollector(_collector);
        }

        private void OnDisable()
        {
            UnregisterCollector();
        }

        private void UpdateScore(int value)
        {
            if (_collector == null)
            {
                return;
            }

            var text = value.ToString("00");
            _scoreLabel.text = text;
        }
    }
}