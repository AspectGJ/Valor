using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BasicStatsPlayer2 : MonoBehaviour, IPersistence
{
    //public DataPersistenceManager dataPersistenceManager;

    //public DataPersistenceManagerLocal localDataPersistenceManager;
    public AttributesScriptableObject playerAttributesOS;
    public GameObject opponent;
    public int attack;
    public int strongAttack;
    public int deepSharpness;

    public int targetHealth;
    public int maxHealth;

    public int targetMana;
    public int maxMana;


    public Image healthBar;
    float healthChangeSpeed = 10f;
    public Image manaBar;
    Notifications notifications;

    void Start()
    {
        if (playerAttributesOS == null)
        {
            playerAttributesOS = Resources.Load<AttributesScriptableObject>("Scripts/ScriptableObjects/Player");
            if (playerAttributesOS == null)
            {
                //Debug.LogError("Player attributes not loaded!");
                return;
            }
        }

        if (DataPersistenceManagerLocal.instance == null)
        {
            //Debug.LogError("localDataPersistenceManager instance not assigned! in fight 2");
            return;
        }

        //localDataPersistenceManager.LoadGame();
        LoadData(DataPersistenceManagerLocal.instance.data);

    }



    public void LoadData(Data data)
    {
        if (playerAttributesOS == null)
        {
            playerAttributesOS = new AttributesScriptableObject();
        }
        playerAttributesOS.healthPoint = data.playerAttributesData.healthPoint;
        targetHealth = playerAttributesOS.healthPoint;
        maxHealth = playerAttributesOS.healthPoint;

        playerAttributesOS.mana = data.playerAttributesData.mana;
        targetMana = playerAttributesOS.mana;
        maxMana = playerAttributesOS.mana;
        //     // Sağlık çubuğunu güncelle
        //healthBar.fillAmount = (float)playerAttributesOS.healthPoint / maxHealth;

        // // Mana çubuğunu güncelle
        //manaBar.fillAmount = (float)playerAttributesOS.mana / maxMana;

        playerAttributesOS.attackmin = data.playerAttributesData.attackmin;
        playerAttributesOS.attackmax = data.playerAttributesData.attackmax;

        playerAttributesOS.StrongAttackmin = data.playerAttributesData.StrongAttackmin;
        playerAttributesOS.StrongAttackmax = data.playerAttributesData.StrongAttackmax;

        playerAttributesOS.DeepSharpnessmin = data.playerAttributesData.DeepSharpnessmin;
        playerAttributesOS.DeepSharpnessmax = data.playerAttributesData.DeepSharpnessmax;
    }
    public void SaveData(Data data)
    {
        //print("Saving player data");
        data.playerAttributesData.healthPoint = playerAttributesOS.healthPoint;
        data.playerAttributesData.mana = playerAttributesOS.mana;

        data.playerAttributesData.attackmin = playerAttributesOS.attackmin;
        data.playerAttributesData.attackmax = playerAttributesOS.attackmax;

        data.playerAttributesData.StrongAttackmin = playerAttributesOS.StrongAttackmin;
        data.playerAttributesData.StrongAttackmax = playerAttributesOS.StrongAttackmax;

        data.playerAttributesData.DeepSharpnessmin = playerAttributesOS.DeepSharpnessmin;
        data.playerAttributesData.DeepSharpnessmax = playerAttributesOS.DeepSharpnessmax;
       // print("Saved player data");
    }

    void Update()
    {


        if (opponent == null)
        {
            SendNotification();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


        if (playerAttributesOS.mana != targetMana)
        {
            playerAttributesOS.mana = (int)Mathf.MoveTowards(playerAttributesOS.mana, targetMana, healthChangeSpeed * Time.deltaTime);
            manaBar.fillAmount = (float)playerAttributesOS.mana / maxMana;
        }
    }

    public bool Attack()
    {
        if (playerAttributesOS.mana >= playerAttributesOS.attackCost)
        {

            DecreaseMana(playerAttributesOS.attackCost);
            attack = RandomNum(playerAttributesOS.attackmin, playerAttributesOS.attackmax);
            opponent.GetComponent<BasicStatsEnemy2>().TakeDamage(attack);

            return true;
        }

        return false;
    }

    public bool StrongAttack()
    {
        if (playerAttributesOS.mana >= playerAttributesOS.StrongAttackCost)
        {

            DecreaseMana(playerAttributesOS.StrongAttackCost);
            strongAttack = RandomNum(playerAttributesOS.StrongAttackmin, playerAttributesOS.StrongAttackmax);
            opponent.GetComponent<BasicStatsEnemy2>().TakeDamage(strongAttack);

            return true;
        }

        return false;

    }

    public bool DeepSharpness()
    {
        if (playerAttributesOS.mana >= playerAttributesOS.DeepSharpnessCost)
        {

            DecreaseMana(playerAttributesOS.DeepSharpnessCost);
            deepSharpness = RandomNum(playerAttributesOS.DeepSharpnessmin, playerAttributesOS.DeepSharpnessmax);
            opponent.GetComponent<BasicStatsEnemy2>().TakeDamage(deepSharpness);
            opponent.GetComponent<BasicStatsEnemy2>().bleedTurns = 3;

            return true;
        }
        return false;
    }

    public void TakeDamage(int damage)
    {
        //playerAttributesOS.healthPoint -= damage;
        targetHealth -= damage;

        targetHealth = Mathf.Clamp(targetHealth, 0, maxHealth);

        if (playerAttributesOS.healthPoint != targetHealth)
        {
            playerAttributesOS.healthPoint = targetHealth;
            healthBar.fillAmount = (float)playerAttributesOS.healthPoint / maxHealth;
        }

    }

    public void SendNotification()
    {
        notifications = new Notifications();
        notifications.ScheduleNotification(DateTime.Now.AddSeconds(1));
    }

    public int RandomNum(int min, int max)
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(min, max);
        return randomNumber;
    }

    public void IncreaseMana(int amount)
    {
        targetMana += amount;

        targetMana = Mathf.Clamp(targetMana, 0, maxMana);

        if(playerAttributesOS.mana != targetMana)
        {
            playerAttributesOS.mana = targetMana;
            manaBar.fillAmount = (float)playerAttributesOS.mana / maxMana;
        }
    }

    public void IncreaseHealth(int amount)
    {
        targetHealth += amount;

        targetHealth = Mathf.Clamp(targetHealth, 0, maxHealth);

        if (playerAttributesOS.healthPoint != targetHealth)
        {
            playerAttributesOS.healthPoint = targetHealth;
            healthBar.fillAmount = (float)playerAttributesOS.healthPoint / maxHealth;
        }
    }

    public void DecreaseMana(int amount)
    {
        targetMana -= amount;

        targetMana = Mathf.Clamp(targetMana, 0, maxMana);

    }

}
