using UnityEngine;
using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class ScoreBar : MonoBehaviour, IScoreBar
    {
        [SerializeField] private BoxCollector _collector;

        private UIDocument _ui;
        private Label _scoreLabel;

        public bool CollectorEnabled => _collector != null;

        public bool ScoreEnabled => _scoreLabel != null;

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

        private void Awake()
        {
            _ui = GetComponent<UIDocument>();
            _scoreLabel = _ui.rootVisualElement.Q<Label>("score-label");
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

        private void OnValidate()
        {
            if (_collector != null)
            {
                RegisterCollector(_collector);
            }
            else
            {
                UnregisterCollector();
            }
        }
    }
}