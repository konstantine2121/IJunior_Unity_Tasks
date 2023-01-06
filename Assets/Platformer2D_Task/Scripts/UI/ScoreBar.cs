using UnityEngine;
using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class ScoreBar : MonoBehaviour, IScoreBar
    {
        private const string ScoreLabelName = "score-label";
        [SerializeField] private BoxCollector _collector;

        private VisualElement _rootElement;
        private Label _scoreLabel;

        public bool CollectorEnabled => _collector != null;

        public bool ScoreEnabled => _scoreLabel != null;

        public void RegisterCollector(BoxCollector collector)
        {
            UnregisterCollector();

            if (collector != null)
            {
                _collector = collector;
                _collector.NumberOfBoxChanged += UpdateScore;

                UpdateScore(collector.GearBoxes);
            }
        }

        public void UnregisterCollector()
        {
            if (_collector != null)
            {
                _collector.NumberOfBoxChanged -= UpdateScore;
            }
        }

        private void Awake()
        {
            _rootElement = GetComponent<UIDocument>().rootVisualElement;
            _scoreLabel = _rootElement.Q<Label>(ScoreLabelName);
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