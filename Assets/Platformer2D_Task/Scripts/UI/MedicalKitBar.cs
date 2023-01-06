using UnityEngine;
using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class MedicalKitBar : MonoBehaviour, IMedicalKitBar
    {
        private const string MedkitButtonName = "medkit-button";
        private const string MedkitLabelName = "medkit-label";

        private IMedicalKitCollector _medkitContainer;
        private IHealth _healTarget;

        private VisualElement _rootElement;
        private Label _scoreLabel;

        public bool CollectorEnabled => _medkitContainer != null;

        public bool ScoreEnabled => _scoreLabel != null;

        public Button MedicalKitButton { get; private set; }

        public void RegisterCollector(IMedicalKitCollector collector)
        {
            UnregisterCollector();

            if (collector != null)
            {
                _medkitContainer = collector;
                _medkitContainer.NumberOfKitsChanged += UpdateKits;

                UpdateKits(collector.MedicalKits);
            }
        }

        public void UnregisterCollector()
        {
            if (_medkitContainer != null)
            {
                _medkitContainer.NumberOfKitsChanged -= UpdateKits;
            }
        }

        public void RegisterHealTarget(IHealth healTarget)
        {
            _healTarget = healTarget;
        }

        public void UnregisterHealTarget()
        {
            _healTarget = null;

        }

        private void Awake()
        {
            _rootElement = GetComponent<UIDocument>().rootVisualElement;
            _scoreLabel = _rootElement.Q<Label>(MedkitLabelName);
            MedicalKitButton = _rootElement.Q<Button>(MedkitButtonName);

            MedicalKitButton.clicked += OnMedicalKitButtonClicked;
            MedicalKitButton.focusable = false;
        }

        private void OnMedicalKitButtonClicked()
        {
            var heal = _medkitContainer.UseMedicalKit();
            _healTarget.TakeHeal(heal);
        }

        private void OnEnable()
        {
            RegisterCollector(_medkitContainer);
        }

        private void OnDisable()
        {
            UnregisterCollector();
        }

        private void UpdateKits(int value)
        {
            if (_medkitContainer == null)
            {
                return;
            }

            var text = value.ToString() + "/" + _medkitContainer.MaxMedicalKits;
            _scoreLabel.text = text;

            SetButtonColor(value);
        }

        private void SetButtonColor(int value)
        {
            var alpha = 0.2f;
            Color backColor = new Color(1,0,0,alpha);

            if (value > 0)
            {
                backColor = new Color(0, 1, 0, alpha);
            }

            MedicalKitButton.style.backgroundColor = new StyleColor(backColor);
        }

        private void OnValidate()
        {
            if (_medkitContainer != null)
            {
                RegisterCollector(_medkitContainer);
            }
            else
            {
                UnregisterCollector();
            }
        }
    }
}