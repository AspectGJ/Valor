using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanState : CharacterState
{
    protected Shaman shaman;
    protected ShamanStateMachine stateMachine;
    private string animationBoolName;

    protected bool moving;


    public ShamanState(Shaman shaman, ShamanStateMachine stateMachine, string animationBoolName)
    {
        this.shaman = shaman;
        this.stateMachine = stateMachine;
        this.animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        shaman.anim.SetBool(animationBoolName, true);
    }

    public virtual void Exit()
    {
        shaman.anim.SetBool(animationBoolName, false);
    }

    public virtual void Update()
    {
        moving = shaman.moving;
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

}
