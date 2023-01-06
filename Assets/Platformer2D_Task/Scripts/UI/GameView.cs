using UnityEngine;

namespace Platformer2D_Task.UI
{    
    [RequireComponent(typeof(ScoreBar))]
    [RequireComponent(typeof(PlayerHpBar))]
    public class GameView : BaseMenuView, IGameView
    {
        private PlayerHpBar _playerHpBar;
        private ScoreBar _scoreBar;
        private MedicalKitBar _medicalKitBar;
        
        public IPlayerHpBar PlayerHpBar => _playerHpBar;

        public IScoreBar ScoreBar => _scoreBar;

        public IMedicalKitBar MedicalKitBar => _medicalKitBar;

        public override MenuType MenuType => MenuType.Game;

        private void Awake()
        {
            _playerHpBar = GetComponent<PlayerHpBar>();
            _scoreBar = GetComponent<ScoreBar>();
            _medicalKitBar = GetComponent<MedicalKitBar>();
        }
    }
}
