using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
   
    public PlayerStrongAttackState strongAttackState { get; private set; }
    public PlayerDeepSharpnessState deepSharpnessState { get; private set; }
    AudioSource src;
    #endregion

    private void Awake() {
        stateMachine = new PlayerStateMachine();
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        
        strongAttackState = new PlayerStrongAttackState(this, stateMachine, "Sattack");
        deepSharpnessState = new PlayerDeepSharpnessState(this, stateMachine, "DeepSharpness");
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody2D>();
        stateMachine.Initialize(idleState);
        src = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.PlayerCurrentState.Update();
        Audio(rb.velocity.x);
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

    public void Audio(float move){
        if (move != 0 && !src.isPlaying)
        {
            src.Play();
        }
        else if(move == 0)
        {
            src.Stop();
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
}
