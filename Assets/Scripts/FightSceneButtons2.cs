using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.SceneManagement;

public class FightSceneButtons2 : MonoBehaviour
{
    public GameObject[] players;
    public GameObject playerIcon;
    public Button[] playerButtons;

    public Button[] potionButtons;

    public GameObject shamanIcon;
    public Button[] shamanButtons;

    public bool isPlayerDead = false;
    public bool isShamanDead = false;

     public GameObject enemy;

    public int gameState = 0; // 0 = player's turn, 1 = second player's turn, 2 = enemy's turn
    public bool hasManaIncreased = false;
    public int currentPlayerIndex = 0;


    public TextMeshProUGUI HpotionAmount;
    public TextMeshProUGUI MpotionAmount;

    [SerializeField] Renderer playerRenderer;
    [SerializeField] Renderer shamanRenderer;

    [SerializeField] private CamaraShake cameraShake;
    private float shakeIntensity = 2f;
    private float shakeTime = 0.6f;
    AudioSource btnsrc;
    public AudioSource btnsrc2;
    public AudioSource btnsrc3;
    public AudioSource btnsrc4;
    public AudioSource btnsrc5;
    public AudioSource btnsrc6;


    // Start is called before the first frame update
    void Start()
    {
        btnsrc = GetComponent<AudioSource>();
        //btnsrc2 = transform.GetChild(0).GetComponent<AudioSource>();

        playerRenderer = players[0].GetComponentInChildren<SpriteRenderer>();
        shamanRenderer = players[1].GetComponentInChildren<SpriteRenderer>();
        //SetPlayerButtonsInteractable(true);
        //SetShamanButtonsInteractable(false);
        //ActivateButtons(playerButtons);
        //DeactivateButtons(shamanButtons);

        HpotionAmount.text = ": " + PlayerPrefs.GetInt("HealthPotion").ToString();
        MpotionAmount.text = ": " + PlayerPrefs.GetInt("ManaPotion").ToString();

        SetPlayerUIActive(true);
        SetShamanUIActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //check player's health
        if (players[0].GetComponent<BasicStatsPlayer2>().playerAttributesOS.healthPoint <= 0 && players[1].GetComponent<BasicStatsShaman2>().shamanAttributesOS.healthPoint <= 0)
        {
            // flag for saving
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if (enemy.GetComponent<BasicStatsEnemy2>().enemyAttributesOS.healthPoint <= 0)
        {
            DataPersistenceManagerLocal.instance.SaveGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


        //check if player is dead
        if (players[0].GetComponent<BasicStatsPlayer2>().playerAttributesOS.healthPoint <= 0)
        {
            isPlayerDead = true;
            SetTransparency(playerRenderer, 0.5f); 
        }
        else if(players[1].GetComponent<BasicStatsShaman2>().shamanAttributesOS.healthPoint <= 0)
        {
            isShamanDead = true;
            SetTransparency(shamanRenderer, 0.5f); 
            
        }
    }

    public void ChangeTurn()
    {
        switch (gameState)
        {
            // Player 1's turn
            case 0:
                if (isShamanDead)
                {
                    gameState = 2;
                    StartCoroutine(EnemyTurn());
                }
                else
                {
                    currentPlayerIndex = 1;
                    gameState = 1;
                    StartCoroutine(PlayerTurn());
                }
                
                break;
            // Player 2's turn
            case 1:
                if(!isShamanDead) {
                    gameState = 2;
                    StartCoroutine(EnemyTurn());
                }
                break;
            // Enemy's turn
            case 2:
                if(isPlayerDead)
                {
                    currentPlayerIndex = 1;
                    gameState = 1;
                }
                else {
                    currentPlayerIndex = 0;
                    gameState = 0;
                }
                StartCoroutine(PlayerTurn());
                break;
        }
    }

    IEnumerator PlayerTurn()
    {
        
        if (currentPlayerIndex == 0 && !isPlayerDead)
        {   
            players[currentPlayerIndex].GetComponent<BasicStatsPlayer2>().IncreaseMana(5);
            SetPlayerButtonsInteractable(true);
            SetPlayerUIActive(true);
            SetShamanUIActive(false);
            SetPotionButtonsInteractable(true);
            SetPotionUIActive(true);
        }
        else if (currentPlayerIndex == 1 && !isShamanDead)
        {
            players[currentPlayerIndex].GetComponent<BasicStatsShaman2>().IncreaseMana(5);
            SetShamanButtonsInteractable(true);
            SetShamanUIActive(true);
            SetPlayerUIActive(false);
            SetPotionButtonsInteractable(true);
            SetPotionUIActive(true);
        }

        // Wait for player's action
        yield return new WaitUntil(() => gameState == 0 || gameState == 1);
    }

    IEnumerator EnemyTurn()
    {
        SetPlayerButtonsInteractable(false);
        SetShamanButtonsInteractable(false);
        SetPotionButtonsInteractable(false);

        // Enemy's action phase
        OnBleedTurn();
        

        //if enemy's stunTurns is greater than 0, enemy will not attack
        if (enemy.GetComponent<BasicStatsEnemy2>().stunTurns <= 0)
        {
            EnemyAttackChoice();
        }


        // Simulate delay for enemy actions and animations
        yield return new WaitForSecondsRealtime(1.0f);

        OnStunTurn();

        // Increase player's mana after enemy's turn
        
        /// Wait a bit more before switching back to give visual feedback
        yield return new WaitForSeconds(1.0f);

        // Switch back to player's turn after enemy's actions and mana increase
        //hasManaIncreased = false; // Reset for next enemy turn
        ChangeTurn();
    }

    void SetPlayerButtonsInteractable(bool interactable)
    {
        foreach (Button button in playerButtons)
        {
            button.interactable = interactable;
        }
    }

    void SetShamanButtonsInteractable(bool interactable)
    {
        foreach (Button button in shamanButtons)
        {
            button.interactable = interactable;
        }
    }

    void SetPotionButtonsInteractable(bool interactable)
    {
        foreach (Button button in potionButtons)
        {
            button.interactable = interactable;
        }
    }


    public void EnemyAttackChoice()
    {
        int skillChoice = Random.Range(0, 2);
        int opponentChoice;
        if (isPlayerDead)
        {
            opponentChoice = 1;
        }
        else if(isShamanDead)
        {
            opponentChoice = 0;
        }
        else
        {
            opponentChoice = Random.Range(0, 2);
        }
        
        switch (skillChoice)
        {
            case 0:
                enemy.GetComponent<Enemy>().stateMachine.changeState(enemy.GetComponent<Enemy>().attackState);
                enemy.GetComponent<BasicStatsEnemy2>().Attack(players[opponentChoice]);
                break;
            case 1:
                enemy.GetComponent<Enemy>().stateMachine.changeState(enemy.GetComponent<Enemy>().vanishState);
                enemy.GetComponent<BasicStatsEnemy2>().VanishState(players[opponentChoice]);
                break;
        }

    }

    public void OnAttackInput()
    {

        if (players[0].GetComponent<BasicStatsPlayer2>().Attack())
        {
            players[0].GetComponent<Player>().stateMachine.changeState(players[0].GetComponent<Player>().attackState);
            cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            btnsrc.Play();

            SetPlayerButtonsInteractable(false);
            Invoke(nameof(ChangeTurn), 1.5f);
        }

    }

    public void OnStrongAttackInput()
    {
        if (players[0].GetComponent<BasicStatsPlayer2>().StrongAttack())
        {
            players[0].GetComponent<Player>().stateMachine.changeState(players[0].GetComponent<Player>().strongAttackState);
            cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            //players[0].GetComponent<BasicStatsPlayer2>().StrongAttack();
            btnsrc2.PlayOneShot(btnsrc2.clip);

            SetPlayerButtonsInteractable(false);
            Invoke(nameof(ChangeTurn), 1.5f);
        }
    }

    public void OnDeepSharpnessInput()
    {

        if (players[0].GetComponent<BasicStatsPlayer2>().DeepSharpness())
        {
            players[0].GetComponent<Player>().stateMachine.changeState(players[0].GetComponent<Player>().deepSharpnessState);
            cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            //players[0].GetComponent<BasicStatsPlayer2>().DeepSharpness();
            btnsrc3.PlayOneShot(btnsrc3.clip);

            SetPlayerButtonsInteractable(false);
            Invoke(nameof(ChangeTurn), 1.5f);
        }
    }

    public void OnShamanHeal()
    {
        if (players[1].GetComponent<BasicStatsShaman2>().Heal(players[0]))
        {
            players[1].GetComponent<Shaman>().stateMachine.changeState(players[1].GetComponent<Shaman>().healState);
            cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            btnsrc5.PlayOneShot(btnsrc5.clip);

            SetShamanButtonsInteractable(false);
            Invoke(nameof(ChangeTurn), 1.5f);
        }
    }

    public void OnShamanStun()
    {
        if (players[1].GetComponent<BasicStatsShaman2>().Stun())
        {
            players[1].GetComponent<Shaman>().stateMachine.changeState(players[1].GetComponent<Shaman>().stunState);
            cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            btnsrc6.PlayOneShot(btnsrc6.clip);

            SetShamanButtonsInteractable(false);
            Invoke(nameof(ChangeTurn), 1.5f);
        }
    }

    public void onShamanSHAttack()
    {
        if (players[1].GetComponent<BasicStatsShaman2>().SHAttack())
        {
            players[1].GetComponent<Shaman>().stateMachine.changeState(players[1].GetComponent<Shaman>().SHattackState);
            cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            btnsrc4.PlayOneShot(btnsrc4.clip);

            SetShamanButtonsInteractable(false);
            Invoke(nameof(ChangeTurn), 1.5f);
        }
    }

    //bleed turn check
    public void OnBleedTurn()
    {
        if (enemy.GetComponent<BasicStatsEnemy2>().bleedTurns > 0)
        {
            enemy.GetComponent<BasicStatsEnemy2>().TakeDamage(3);
            enemy.GetComponent<BasicStatsEnemy2>().bleedTurns--;
        }
    }

    public void OnStunTurn()
    {
        if (enemy.GetComponent<BasicStatsEnemy2>().stunTurns > 0)
        {
            enemy.GetComponent<BasicStatsEnemy2>().stunTurns--;
        }
    }

    private void ActivateButtons(Button[] buttons, bool isActive)
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(isActive);
        }
    }

    private void SetShamanUIActive(bool isActive) // if true then shaman's UI is active, else player's UI is active
    {
        shamanIcon.SetActive(isActive);
        ActivateButtons(shamanButtons, isActive);
    }

    private void SetPlayerUIActive(bool isActive) // if true then player's UI is active, else shaman's UI is active
    {
        playerIcon.SetActive(isActive);
        ActivateButtons(playerButtons, isActive);
    }

    private void SetPotionUIActive(bool isActive)
    {
        ActivateButtons(potionButtons, isActive);
    }


    public void OnHealthPotionInput()
    {
        int healthPotion = PlayerPrefs.GetInt("HealthPotion");

        if (healthPotion > 0)
        {
            healthPotion--;
            PlayerPrefs.SetInt("HealthPotion", healthPotion);
            if (currentPlayerIndex == 0)
            {
                players[0].GetComponent<BasicStatsPlayer2>().IncreaseHealth(20);
            }
            else if(currentPlayerIndex == 1)
            {
                players[1].GetComponent<BasicStatsShaman2>().IncreaseHealth(20);
            }
            
            

            HpotionAmount.text = ": " + healthPotion.ToString();
        }

    }

    public void OnManaPotionInput()
    {
        int manaPotion = PlayerPrefs.GetInt("ManaPotion");

        if (manaPotion > 0)
        {
            manaPotion--;
            PlayerPrefs.SetInt("ManaPotion", manaPotion);
            if (currentPlayerIndex == 0)
            {
                players[0].GetComponent<BasicStatsPlayer2>().IncreaseMana(20);
            }
            else if (currentPlayerIndex == 1)
            {
                players[1].GetComponent<BasicStatsShaman2>().IncreaseMana(20);
            }

            MpotionAmount.text = ": " + manaPotion.ToString();
        }


    }

    private void SetTransparency(Renderer renderer, float alpha)
    {
        Color color = renderer.material.color;
        color.a = alpha;
        renderer.material.color = color;
    }
}
