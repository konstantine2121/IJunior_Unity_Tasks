using UnityEngine;

namespace Platformer2D_Task
{

    public class GUI : MonoBehaviour
    {
        [SerializeField] private PlayerHPBar _playerHPBar;

        [SerializeField] private ScoreBar _scoreBar;

        public PlayerHPBar PlayerHPBar => _playerHPBar;

        public ScoreBar ScoreBar => _scoreBar;
    }
}