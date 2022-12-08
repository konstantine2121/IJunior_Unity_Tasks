using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(Gundam))]
    [RequireComponent(typeof(GundamPatrol))]
    [RequireComponent(typeof(Animator))]
    public class GundamAnimator : MonoBehaviour
    {
        private const string WalkParam = "Walk";

        private GundamPatrol _patrol;
        private Animator _animator;

        private bool Walk
        {
            get { return GetBoolValue(WalkParam); }
            set { SetBoolValue(WalkParam, value); }
        }

        private void Awake()
        {
            _patrol = GetComponent<GundamPatrol>();
            _animator = GetComponent<Animator>();         

            _patrol.StateChanged += StateChanged;
        }

        private void StateChanged(GundamState state)
        {  
            switch (state)
            {
                case GundamState.Idle:
                    SetIdleState();
                    break;
                case GundamState.Walk:
                    SetWalkState();
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
            Walk = false;
        }

        private void SetWalkState()
        {
            Walk = true;
        }
    }
}