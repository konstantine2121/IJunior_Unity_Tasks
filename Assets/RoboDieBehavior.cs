using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RoboDieBehavior : StateMachineBehaviour
{
    static int _roboDeath = Animator.StringToHash("RoboDeath");
    static int _roboRun = Animator.StringToHash("RoboRun");
    static int _roboJump = Animator.StringToHash("RoboJump");
    static int _roboIdle = Animator.StringToHash("RoboIdle");

    int[] _notInterestingOnes = new int[] {
        _roboIdle,
        _roboRun,
        _roboJump,
    };

    private void TryCatchDebug(AnimatorStateInfo stateInfo)
    {
        if (_notInterestingOnes.Any(x => x == stateInfo.shortNameHash))
        {
            return;
        }

        if (stateInfo.shortNameHash == _roboDeath)
        {
            int d = 0;
        }
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //TryCatchDebug(stateInfo);
        //OK!
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //TryCatchDebug(stateInfo);
        //OK!
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TryCatchDebug(stateInfo);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //TryCatchDebug(stateInfo);
        //OK!
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TryCatchDebug(stateInfo);
    }
}
