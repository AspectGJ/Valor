using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : CharacterState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    private string animationBoolName;

    protected float xInput;


    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animationBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        enemy.anim.SetBool(animationBoolName, true);
    }

    public virtual void Exit()
    {
        enemy.anim.SetBool(animationBoolName, false);
    }

    public virtual void Update()
    {
       
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
}
