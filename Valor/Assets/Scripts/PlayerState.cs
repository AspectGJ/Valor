using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    private string animationBoolName;

    protected float xInput;


    public PlayerState(Player player, PlayerStateMachine stateMachine, string animationBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.anim.SetBool(animationBoolName, true);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animationBoolName, false);
    }

    public virtual void Update()
    {
        xInput = player.xPInput;
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
    
}
