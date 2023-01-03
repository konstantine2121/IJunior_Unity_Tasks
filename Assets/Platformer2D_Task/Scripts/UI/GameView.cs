using UnityEngine;

namespace Platformer2D_Task.UI
{    
    [RequireComponent(typeof(ScoreBar))]
    [RequireComponent(typeof(PlayerHpBar))]
    class GameView : BaseMenuView, IGameView
    {
        private PlayerHpBar _playerHpBar;
        private ScoreBar _scoreBar;
        
        public IPlayerHpBar PlayerHpBar => _playerHpBar;

        public IScoreBar ScoreBar => _scoreBar;

        public override MenuType MenuType => MenuType.Game;

        private new void Awake()
        {
            base.Awake();
            _playerHpBar = GetComponent<PlayerHpBar>();
            _scoreBar = GetComponent<ScoreBar>();
        }
    }
}
