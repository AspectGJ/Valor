using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightSceneButtons : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public bool isPlayerTurn = true;
    public bool hasManaIncreased = false;
    
    public Button[] playerButtons;

    [SerializeField] private CamaraShake cameraShake;
    private float shakeIntensity = 2f;
    private float shakeTime = 0.6f;
    AudioSource btnsrc;
    public AudioSource btnsrc2;
    public AudioSource btnsrc3;


    // Start is called before the first frame update
    void Start()
    {
        btnsrc = GetComponent<AudioSource>();
        //btnsrc2 = transform.GetChild(0).GetComponent<AudioSource>();

        SetPlayerButtonsInteractable(true);

    }

    // Update is called once per frame
    void Update()
    {
       

       
    }

    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;

        if (!isPlayerTurn)
        {
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        SetPlayerButtonsInteractable(false);

        // Enemy's action phase
        OnBleedTurn();

        EnemyAttackChoice();


        // Simulate delay for enemy actions and animations
        yield return new WaitForSecondsRealtime(1.0f);

        // Increase player's mana after enemy's turn
        player.GetComponent<BasicStats>().IncreaseMana();
        

        /// Wait a bit more before switching back to give visual feedback
        yield return new WaitForSeconds(1.0f);

        // Switch back to player's turn after enemy's actions and mana increase
        ChangeTurnToPlayer();
    }

    void ChangeTurnToPlayer()
    {
        // This method now directly sets the state without being a coroutine.
        StartCoroutine(DelayedPlayerTurnActivation());
    }

    IEnumerator DelayedPlayerTurnActivation()
    {
        // Optionally, wait for additional time here if needed for animations or other reasons
        yield return new WaitForSeconds(1.0f);

        SetPlayerButtonsInteractable(true);
        isPlayerTurn = true;
        hasManaIncreased = false; // Reset for next enemy turn
    }

    void SetPlayerButtonsInteractable(bool interactable)
    {
        foreach (Button button in playerButtons)
        {
            button.interactable = interactable;
        }
    }


    public void EnemyAttackChoice()
    {
        int choice = Random.Range(0, 2);
        switch (choice)
        {
            case 0:
                enemy.GetComponent<Enemy>().stateMachine.changeState(enemy.GetComponent<Enemy>().attackState);
                enemy.GetComponent<BasicStatcEnemy>().Attack();

                break;
            case 1:
                enemy.GetComponent<Enemy>().stateMachine.changeState(enemy.GetComponent<Enemy>().vanishState);
                enemy.GetComponent<BasicStatcEnemy>().VanishState();

                break;
            
        }
    }

    
    

    public void OnAttackInput()
    {

        if (player.GetComponent<BasicStats>().Attack())
        {
            player.GetComponent<Player>().stateMachine.changeState(player.GetComponent<Player>().attackState);
            cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            btnsrc.Play();

            Invoke(nameof(ChangeTurn), 1.5f);
        }

        
    }

    public void OnStrongAttackInput()
    {

        if (player.GetComponent<BasicStats>().StrongAttack())
        {
            player.GetComponent<Player>().stateMachine.changeState(player.GetComponent<Player>().strongAttackState);
            cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            //player.GetComponent<BasicStats>().StrongAttack();
            btnsrc2.PlayOneShot(btnsrc2.clip);
            Invoke(nameof(ChangeTurn), 1.5f);
        }
        

    }

    public void OnDeepSharpnessInput()
    {

        if (player.GetComponent<BasicStats>().DeepSharpness())
        {
            player.GetComponent<Player>().stateMachine.changeState(player.GetComponent<Player>().deepSharpnessState);
            cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            //player.GetComponent<BasicStats>().DeepSharpness();
            btnsrc3.PlayOneShot(btnsrc3.clip);
            Invoke(nameof(ChangeTurn), 1.5f);
        }
        
    }


    

    //bleed turn check
    public void OnBleedTurn()
    {
        if (enemy.GetComponent<BasicStatcEnemy>().bleedTurns > 0)
        {
            enemy.GetComponent<BasicStatcEnemy>().TakeDamage(3);
            enemy.GetComponent<BasicStatcEnemy>().bleedTurns--;
        }
    }



}
