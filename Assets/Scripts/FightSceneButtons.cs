using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.ComponentModel;
using UnityEngine.SceneManagement;

public class FightSceneButtons : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public bool isPlayerTurn = true;
    public bool hasManaIncreased = false;
    
    public Button[] playerButtons;

    

    public TextMeshProUGUI HpotionAmount;
    public TextMeshProUGUI MpotionAmount;

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

        HpotionAmount.text = ": " + PlayerPrefs.GetInt("HealthPotion").ToString();
        MpotionAmount.text = ": " + PlayerPrefs.GetInt("ManaPotion").ToString();

    }

    // Update is called once per frame
    void Update()
    {

        if (player.GetComponent<BasicStats>().playerAttributesOS.healthPoint <= 0)
        {
            //load previoud scene
            //DataPersistenceManagerLocal.instance.SaveGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if (enemy.GetComponent<BasicStatcEnemy>().enemyAttributesOS.healthPoint <= 0)
        {
            //load next scene
            DataPersistenceManagerLocal.instance.SaveGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

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
        //player.GetComponent<BasicStats>().IncreaseMana(2);
        

        /// Wait a bit more before switching back to give visual feedback
        yield return new WaitForSeconds(1.0f);

        // Switch back to player's turn after enemy's actions and mana increase
        ChangeTurnToPlayer();
    }

    void ChangeTurnToPlayer()
    {
        // This method now directly sets the state without being a coroutine.
        //player.GetComponent<BasicStats>().IncreaseMana(2);
        StartCoroutine(DelayedPlayerTurnActivation());
    }

    IEnumerator DelayedPlayerTurnActivation()
    {
        // Optionally, wait for additional time here if needed for animations or other reasons
        yield return new WaitForSeconds(1.0f);

        SetPlayerButtonsInteractable(true);
        player.GetComponent<BasicStats>().IncreaseMana(2);
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

            SetPlayerButtonsInteractable(false);
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

            SetPlayerButtonsInteractable(false);
            Invoke(nameof(ChangeTurn), 1.5f);
        }
        

    }

    public void OnDeepSharpnessInput()
    {

        if (player.GetComponent<BasicStats>().DeepSharpness())
        {
            player.GetComponent<Player>().stateMachine.changeState(player.GetComponent<Player>().deepSharpnessState);
            cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            
            btnsrc3.PlayOneShot(btnsrc3.clip);

            SetPlayerButtonsInteractable(false);
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

    public void OnHealthPotionInput()
    {
        int healthPotion = PlayerPrefs.GetInt("HealthPotion");

        if(healthPotion > 0)
        {
            healthPotion--;
            PlayerPrefs.SetInt("HealthPotion", healthPotion);
            player.GetComponent<BasicStats>().IncreaseHealth(20);

            HpotionAmount.text = ": " + healthPotion.ToString();
        }
        
    }

    public void OnManaPotionInput()
    {
        int manaPotion = PlayerPrefs.GetInt("ManaPotion");

        if(manaPotion > 0)
        {
            manaPotion--;
            PlayerPrefs.SetInt("ManaPotion", manaPotion);
            player.GetComponent<BasicStats>().IncreaseMana(20);

            MpotionAmount.text = ": " + manaPotion.ToString();
        }
          
        
    }
}
