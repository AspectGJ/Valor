using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : CharacterStateMachine
{
    public PlayerState PlayerCurrentState { get; private set; }
    

    public void Initialize(PlayerState startingState)
    {
        PlayerCurrentState = startingState;
        PlayerCurrentState.Enter();
    }

    public void changeState(PlayerState newState)
    {
        PlayerCurrentState.Exit();
        PlayerCurrentState = newState;
        PlayerCurrentState.Enter();
    }


}
