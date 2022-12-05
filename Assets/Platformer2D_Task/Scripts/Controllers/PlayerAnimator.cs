using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private const string RunParam = "Run";
        private const string JumpParam = "Jump";
        private const string DeathParam = "Death";

        private PlayerMovement _movement;
        private Animator _animator;

        private bool Run
        {
            get { return GetBoolValue(RunParam); }
            set { SetBoolValue(RunParam, value); }
        }

        private bool Jump
        {
            get { return GetBoolValue(JumpParam); }
            set { SetBoolValue(JumpParam, value); }
        }

        //private 
        //RuntimeAnimatorController _controller;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _animator = GetComponent<Animator>();         

            _movement.StateChanged += StateChanged;
        }

        private void StateChanged(PlayerState state)
        {  
            switch (state)
            {
                case PlayerState.Idle:
                    SetIdleState();
                    break;
                case PlayerState.Run:
                    SetRunState();
                    break;
                case PlayerState.Jump:
                    SetJumpState();
                    break;
                case PlayerState.Dead:
                    SetDeadState();
                    break;
            };
        }

        private void SetBoolValue(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        private bool GetBoolValue(string name)
        {
            return _animator.GetBool(name);
        }

        private void SetIdleState()
        {
            Run = false;
            Jump = false;
        }

        private void SetRunState()
        {
            Run = true;
            Jump = false;
        }

        private void SetJumpState()
        {
            Run = false;
            Jump = true;
        }

        private void SetDeadState()
        {
            Run = false;
            Jump = false;
            _animator.SetTrigger(DeathParam);
        }
    }
}