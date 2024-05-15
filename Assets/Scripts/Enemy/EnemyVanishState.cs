using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVanishState : EnemyState
{
    private bool isAttackAnimationFinished;




    public EnemyVanishState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animationBoolName) : base(_enemy, _stateMachine, _animationBoolName)
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
        if (stateInfo.IsName("Vanish") && stateInfo.normalizedTime >= 1f)
        {
            isAttackAnimationFinished = true;
        }
        if (isAttackAnimationFinished)
        {

            stateMachine.changeState(enemy.idleState);
        }
    }
}
