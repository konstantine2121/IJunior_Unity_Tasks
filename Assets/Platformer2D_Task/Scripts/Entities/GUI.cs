using Platformer2D_Task.UI;
using UnityEngine;

namespace Platformer2D_Task
{

    public class GUI : MonoBehaviour
    {
        [SerializeField] private PlayerHpBar _playerHpBar;

        [SerializeField] private ScoreBar _scoreBar;

        public IPlayerHpBar PlayerHpBar => _playerHpBar;

        public IScoreBar ScoreBar => _scoreBar;
    }
}