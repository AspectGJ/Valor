using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanIdleState : ShamanState
{
    public ShamanIdleState(Shaman _shaman, ShamanStateMachine _stateMachine, string _animationBoolName) : base(_shaman, _stateMachine, _animationBoolName)
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
        if (moving)
        {
            stateMachine.changeState(shaman.moveState);
        }
        
    }




}
