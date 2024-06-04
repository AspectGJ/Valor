using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanMoveState : ShamanState
{
    public ShamanMoveState(Shaman _shaman, ShamanStateMachine _stateMachine, string _animationBoolName) : base(_shaman, _stateMachine, _animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        shaman.flipRenderer();

        if (!moving)
        {
            stateMachine.changeState(shaman.idleState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
