using System;
using Platformer2D_Task.UI;
using UnityEngine;

namespace Platformer2D_Task
{
    public class GUI : MonoBehaviour
    {
        [SerializeField] private PlayerHpBar _playerHpBar;
        [SerializeField] private MedicalKitBar _medicalKitBar;
        [SerializeField] private ScoreBar _scoreBar;
        [SerializeField] private MenuContainer _menuContainer;
        [SerializeField] private MenuController _menuController;

        private IPlayer _player;

        public IPlayerHpBar PlayerHpBar => _playerHpBar;

        public IScoreBar ScoreBar => _scoreBar;

        public IMenuContainer MenuContainer => _menuContainer;

        public IMedicalKitBar MedicalKitBar => _medicalKitBar;

        public void RegisterPlayer(IPlayer player)
        {
            if (_player != null)
            {
                UnregisterPlayer();
            }

            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            _player = player;

            PlayerHpBar.RegisterHealth(player.Health);
            ScoreBar.RegisterCollector(player.BoxCollector);
            MedicalKitBar.RegisterCollector(player.MedicalKitCollector);
            MedicalKitBar.RegisterHealTarget(player.Health);
            _menuContainer.GameOver.RegisterCollector(player.BoxCollector);
        }

        public void RegisterController(IGameController gameRestarter)
        {
            _menuController.GameController = gameRestarter;
        }

        public void UnregisterPlayer()
        {
            PlayerHpBar.UnregisterHealth();
            ScoreBar.UnregisterCollector();
            MedicalKitBar.UnregisterCollector();
            MedicalKitBar.UnregisterHealTarget();
            _menuContainer.GameOver.UnregisterCollector();
        }

        private void OnEnable()
        {
            if (_player != null)
            {
                RegisterPlayer(_player);
            }
        }

        private void OnDisable()
        {
            UnregisterPlayer();
        }
    }
}