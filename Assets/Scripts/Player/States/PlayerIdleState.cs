using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
  public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animationBoolName) : base(_player, _stateMachine, _animationBoolName)
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
    if (xInput != 0)
    {
      stateMachine.changeState(player.moveState);
    }
    
  }

    


}
