using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStabState : EnemyState
{
    private bool isAttackAnimationFinished;




    public EnemyStabState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animationBoolName) : base(_enemy, _stateMachine, _animationBoolName)
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
        AnimatorStateInfo stateInfo = enemy.anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Stab") && stateInfo.normalizedTime >= 1f)
        {
            isAttackAnimationFinished = true;
        }
        if (isAttackAnimationFinished)
        {

            stateMachine.changeState(enemy.idleState);
        }
    }
}
