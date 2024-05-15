using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shaman : MonoBehaviour
{
    [Header("Shaman Movement Variables")]
    public float moveSpeed = 5f;

    public bool moving;

    // Region for the components and references
    #region Components and References
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    // Region for the states of the player
    #region States
    public ShamanStateMachine stateMachine { get; private set; }
    public ShamanMoveState moveState { get; private set; }
    public ShamanIdleState idleState { get; private set; }

    public ShamanHealState healState { get; private set; }
   // public PlayerAttackState attackState { get; private set; }


    //public PlayerStrongAttackState strongAttackState { get; private set; }
   // public PlayerDeepSharpnessState deepSharpnessState { get; private set; }
    
    #endregion

    private void Awake()
    {
        stateMachine = new ShamanStateMachine();
        moveState = new ShamanMoveState(this, stateMachine, "Move");
        idleState = new ShamanIdleState(this, stateMachine, "Idle");
        healState = new ShamanHealState(this, stateMachine, "Heal");
        //attackState = new PlayerAttackState(this, stateMachine, "Attack");

        //strongAttackState = new PlayerStrongAttackState(this, stateMachine, "Sattack");
        //deepSharpnessState = new PlayerDeepSharpnessState(this, stateMachine, "DeepSharpness");
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
        stateMachine.ShamanCurrentState.Update();
        
    }
    public void setVelocity(float xInput)
    {
        
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

    

    
}
