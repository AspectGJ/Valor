using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : CharacterStateMachine
{
    public EnemyState EnemyCurrentState { get; private set; }
    

    public void Initialize(EnemyState startingState)
    {
        EnemyCurrentState = startingState;
        EnemyCurrentState.Enter();
    }

    public void changeState(EnemyState newState)
    {
        EnemyCurrentState.Exit();
        EnemyCurrentState = newState;
        EnemyCurrentState.Enter();
    }

    
}
