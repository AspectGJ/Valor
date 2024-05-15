using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    // Region for the components and references
    #region Components and References
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    // Region for the states of the player
    #region States
    public EnemyStateMachine stateMachine { get; private set; }
    
    public EnemyIdleState idleState { get; private set; }
    public EnemyAttackState attackState { get; private set; }
    //public EnemyStabState stabState { get; private set; }
    public EnemyVanishState vanishState { get; private set; }
    //public PlayerBlockState blockState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
        
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        attackState = new EnemyAttackState(this, stateMachine, "Attack");
        //stabState = new EnemyStabState(this, stateMachine, "Stab");
        vanishState = new EnemyVanishState(this, stateMachine, "Vanish");
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.EnemyCurrentState.Update();
    }
}
