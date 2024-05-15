using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Globalization;

public class FightSceneButtons2 : MonoBehaviour
{
    public GameObject[] players;
    public GameObject playerIcon;
    public Button[] playerButtons;

    public GameObject shamanIcon;
    public Button[] shamanButtons;

     public GameObject enemy;

    public int gameState = 0; // 0 = player's turn, 1 = second player's turn, 2 = enemy's turn
    public bool hasManaIncreased = false;
    private int currentPlayerIndex = 0;
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


        //SetPlayerButtonsInteractable(true);
        //SetShamanButtonsInteractable(false);
        //ActivateButtons(playerButtons);
        //DeactivateButtons(shamanButtons);
        SetPlayerUIActive(true);
        SetShamanUIActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeTurn()
    {
        switch (gameState)
        {
            // Player 1's turn
            case 0:
                currentPlayerIndex = 1;
                gameState = 1;
                StartCoroutine(PlayerTurn());
                break;
            // Player 2's turn
            case 1:
                gameState = 2;
                StartCoroutine(EnemyTurn());
                break;
            // Enemy's turn
            case 2:
                currentPlayerIndex = 0;
                gameState = 0;
                StartCoroutine(PlayerTurn());
                break;
        }
    }

    IEnumerator PlayerTurn()
    {
        if (currentPlayerIndex == 0)
        {
            SetPlayerButtonsInteractable(true);
            SetPlayerUIActive(true);
            SetShamanUIActive(false);
        }
        else
        {
            SetShamanButtonsInteractable(true);
            SetShamanUIActive(true);
            SetPlayerUIActive(false);
        }

        // Wait for player's action
        yield return new WaitUntil(() => gameState == 0 || gameState == 1);
    }

    IEnumerator EnemyTurn()
    {
        SetPlayerButtonsInteractable(false);
        SetShamanButtonsInteractable(false);

        // Enemy's action phase
        OnBleedTurn();

        EnemyAttackChoice();

        // Simulate delay for enemy actions and animations
        yield return new WaitForSecondsRealtime(1.0f);

        // Increase player's mana after enemy's turn
        if (currentPlayerIndex == 0)
        {
            players[currentPlayerIndex].GetComponent<BasicStatsPlayer2>().IncreaseMana();
        }
        else
        {
            players[currentPlayerIndex].GetComponent<BasicStatsShaman2>().IncreaseMana();
        }

        /// Wait a bit more before switching back to give visual feedback
        yield return new WaitForSeconds(1.0f);

        // Switch back to player's turn after enemy's actions and mana increase
        hasManaIncreased = false; // Reset for next enemy turn
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


    public void EnemyAttackChoice()
    {
        int skillChoice = Random.Range(0, 2);
        int opponentChoice = Random.Range(0, 2);
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

            Invoke(nameof(ChangeTurn), 1.5f);
        }
    }

    public void OnShamanFirstSkill()
    {
        if (players[1].GetComponent<BasicStatsShaman2>().FirstSkill())
        {
            //players[currentPlayerIndex].GetComponent<Player>().stateMachine.changeState(players[currentPlayerIndex].GetComponent<Player>().attackState);
            //cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            //btnsrc.Play();

            Debug.Log("First skill activated!");
            Invoke(nameof(ChangeTurn), 1.5f);
        }
    }

    public void OnShamanSecondSkill()
    {
        if (players[1].GetComponent<BasicStatsShaman2>().SecondSkill())
        {
            //players[currentPlayerIndex].GetComponent<Player>().stateMachine.changeState(players[currentPlayerIndex].GetComponent<Player>().attackState);
            //cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            //btnsrc.Play();

            Debug.Log("Second skill activated!");
            Invoke(nameof(ChangeTurn), 1.5f);
        }
    }

    public void onShamanThirdSkill()
    {
        if (players[1].GetComponent<BasicStatsShaman2>().ThirdSkill())
        {
            //players[currentPlayerIndex].GetComponent<Player>().stateMachine.changeState(players[currentPlayerIndex].GetComponent<Player>().attackState);
            //cameraShake.ShakeCamera(shakeIntensity, shakeTime);
            //btnsrc.Play();

            Debug.Log("Third skill activated!");
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
}
