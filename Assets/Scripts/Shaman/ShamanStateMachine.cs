using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanStateMachine : CharacterStateMachine
{
    public ShamanState ShamanCurrentState { get; private set; }


    public void Initialize(ShamanState startingState)
    {
        ShamanCurrentState = startingState;
        ShamanCurrentState.Enter();
    }

    public void changeState(ShamanState newState)
    {
        ShamanCurrentState.Exit();
        ShamanCurrentState = newState;
        ShamanCurrentState.Enter();
    }


}
