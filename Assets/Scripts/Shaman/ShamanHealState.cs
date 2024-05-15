using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Windows;

public class ShamanHealState : ShamanState
{
    private bool isAttackAnimationFinished;

    

    public ShamanHealState(Shaman _player, ShamanStateMachine _stateMachine, string _animationBoolName) : base(_player, _stateMachine, _animationBoolName)
    {
        isAttackAnimationFinished = false;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        isAttackAnimationFinished = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        AnimatorStateInfo stateInfo = shaman.anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Heal") && stateInfo.normalizedTime >= 1f)
        {
            isAttackAnimationFinished = true;
        }
        if (isAttackAnimationFinished)
        {

            stateMachine.changeState(shaman.idleState);
        }
    }
}
