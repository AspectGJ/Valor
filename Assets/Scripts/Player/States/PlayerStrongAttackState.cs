using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerStrongAttackState : PlayerState
{
    private bool isAttackAnimationFinished;

    


    public PlayerStrongAttackState(Player _player, PlayerStateMachine _stateMachine, string _animationBoolName) : base(_player, _stateMachine, _animationBoolName)
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
        AnimatorStateInfo stateInfo = player.anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Sattack") && stateInfo.normalizedTime >= 1f)
        {
            isAttackAnimationFinished = true;
        }
        if (isAttackAnimationFinished)
        {

            stateMachine.changeState(player.idleState);
        }
    }
}
