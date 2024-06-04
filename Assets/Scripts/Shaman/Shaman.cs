using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shaman : MonoBehaviour
{
    [Header("Shaman Movement Variables")]

    //public Collider2D shamanCollider;

    public GameObject player; // Reference to the player
    public Transform playerTransform; // Reference to the player's transform
    public bool playerFlipX = false; // Reference to the player's flipX value
    public float followSpeed = 5f; // Speed at which the sidekick follows the player
    public float followDistance = 2f; // Minimum distance between the player and sidekick


    bool isInFightScene; // Check if the sidekick is in the fight scene

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
    public ShamanStunState stunState { get; private set; }
    public ShamanSHAttackState SHattackState { get; private set; }


    #endregion

    private void Awake()
    {
        stateMachine = new ShamanStateMachine();
        moveState = new ShamanMoveState(this, stateMachine, "Move");
        idleState = new ShamanIdleState(this, stateMachine, "Idle");
        healState = new ShamanHealState(this, stateMachine, "Heal");
        stunState = new ShamanStunState(this, stateMachine, "Stun");
        SHattackState = new ShamanSHAttackState(this, stateMachine, "SHAttack");

    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody2D>();
        stateMachine.Initialize(idleState);

        //get current scene number
        int scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        switch (scene)
        {
            
            case 1:
                isInFightScene = false;
                break;
            case 2:
                isInFightScene = true;
                break;
            case 3:
                isInFightScene = false;
                break;
            case 4:
                isInFightScene = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.ShamanCurrentState.Update();
        if (!isInFightScene)
        {
            playerFlipX = player.GetComponentInChildren<SpriteRenderer>().flipX;
        }
        
    }

    void FixedUpdate()
    {
        if (playerTransform != null && !isInFightScene)
        {
            // Calculate the direction towards the player
            Vector3 direction = playerTransform.position - transform.position;
            float distance = direction.magnitude; // Calculate the distance between the player and sidekick

            // Check if the distance is greater than the follow distance
            if (distance > followDistance)
            {
                // Normalize the direction vector
                direction.Normalize();

                // Move the sidekick towards the player
                transform.position += direction * followSpeed * Time.deltaTime;
                moving = true; 
            }
            else {
                moving = false;           
            }
        }
    }
    public void flipRenderer()
    {
        //if xInput is minus, make flip X in sprite renderer true
        if (playerFlipX && !isInFightScene)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the sidekick collides with the player, ignore the collision
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponentInChildren<Collider2D>());
        }
    }

}
