using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player Movement Variables")]
    public float moveSpeed = 5f;
    public float xPInput;

    
    

    // Region for the components and references
    #region Components and References
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    // Region for the states of the player
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerBlockState blockState { get; private set; }
    #endregion

    private void Awake() {
        stateMachine = new PlayerStateMachine();
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        blockState = new PlayerBlockState(this, stateMachine, "Block");
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
        stateMachine.CurrentState.Update();
    }
    public void setVelocity(float xInput)
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        //if xInput is minus, make flip X in sprite renderer true
        if (xInput < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else if (xInput > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }


        
    }

    public void goRight()
    {
        xPInput = 1;
    }

    public void goNone()
    {
          xPInput = 0;
    }

    public void goLeft()
    {
        xPInput = -1;
    }

    public void OnAttackInput()
    {
        stateMachine.changeState(attackState);
    }

    public void onBlockInput()
    {
        stateMachine.changeState(blockState);
    }

}
