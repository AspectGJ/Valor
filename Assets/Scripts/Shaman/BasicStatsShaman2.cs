using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class BasicStatsShaman2 : MonoBehaviour ,IPersistence
{
    public AttributesScriptableObject shamanAttributesOS;
    //public DataPersistenceManager dataPersistenceManager;
    //public DataPersistenceManagerLocal localDataPersistenceManager;
    public GameObject opponent;
    public int Healskill;
    public int Stunskill;
    public int SHAttackskill;

    public int targetHealth;
    int maxHealth;

    public int targetMana;
    public int maxMana;


    public Image healthBar;
    float healthChangeSpeed = 10f;
    public Image manaBar;


    Notifications notifications;


    private void Start() {
        if (shamanAttributesOS == null)
    {
        shamanAttributesOS = Resources.Load<AttributesScriptableObject>("Scripts/ScriptableObjects/Shaman"); // Örnek bir kaynak adı
        if (shamanAttributesOS == null)
        {
            //Debug.LogError("Player attributes not loaded!");
            return;
        }
    }

    if (DataPersistenceManagerLocal.instance == null)
    {
        //Debug.LogError("localDataPersistenceManager instance not assigned!");
        return;
    }

    //localDataPersistenceManager.LoadGame();
    LoadData(DataPersistenceManagerLocal.instance.data);
    }
    public void LoadData(Data data)
    {
        if (shamanAttributesOS == null)
        {
            shamanAttributesOS = new AttributesScriptableObject();
        }
        shamanAttributesOS.healthPoint = data.shamanAttributesData.healthPoint;
        targetHealth = shamanAttributesOS.healthPoint;
        maxHealth = shamanAttributesOS.healthPoint;

        shamanAttributesOS.mana = data.shamanAttributesData.mana;
        targetMana = shamanAttributesOS.mana;
        maxMana = shamanAttributesOS.mana;

    

        shamanAttributesOS.attackmin = data.shamanAttributesData.attackmin;
        shamanAttributesOS.attackmax = data.shamanAttributesData.attackmax;

        shamanAttributesOS.StrongAttackmin = data.shamanAttributesData.StrongAttackmin;
        shamanAttributesOS.StrongAttackmax = data.shamanAttributesData.StrongAttackmax;

        shamanAttributesOS.DeepSharpnessmin = data.shamanAttributesData.DeepSharpnessmin;
        shamanAttributesOS.DeepSharpnessmax = data.shamanAttributesData.DeepSharpnessmax;
        //Debug.Log("Target HP: " + targetHealth + " Max HP: " + maxHealth + "Current Health: " + shamanAttributesOS.healthPoint);
    }

    public void SaveData(Data data)
    {
        //print("Saving shaman data");
        data.shamanAttributesData.healthPoint = shamanAttributesOS.healthPoint;
        data.shamanAttributesData.mana = shamanAttributesOS.mana;

        data.shamanAttributesData.attackmin = shamanAttributesOS.attackmin;
        data.shamanAttributesData.attackmax = shamanAttributesOS.attackmax;

        data.shamanAttributesData.StrongAttackmin = shamanAttributesOS.StrongAttackmin;
        data.shamanAttributesData.StrongAttackmax = shamanAttributesOS.StrongAttackmax;

        data.shamanAttributesData.DeepSharpnessmin = shamanAttributesOS.DeepSharpnessmin;
        data.shamanAttributesData.DeepSharpnessmax = shamanAttributesOS.DeepSharpnessmax;
        //print("Saved shaman data");
    }

    void Update()
    {
        if (shamanAttributesOS.healthPoint <= 0)
        {
            //Destroy(gameObject);
        }

        if (opponent != null)
        {

        }
        else
        {
            SendNotification();
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

        
        if (shamanAttributesOS.mana != targetMana)
        {
            shamanAttributesOS.mana = (int)Mathf.MoveTowards(shamanAttributesOS.mana, targetMana, healthChangeSpeed * Time.deltaTime);
            manaBar.fillAmount = (float)shamanAttributesOS.mana / maxMana;
        }
    }

    public bool Heal(GameObject player)
    {
        if (shamanAttributesOS.mana >= shamanAttributesOS.HealCost)
        {
            DecreaseMana(shamanAttributesOS.HealCost);
            Healskill = RandomNum(shamanAttributesOS.Healmin, shamanAttributesOS.Healmax);

            float shamanHealthPercentage = (float)targetHealth / maxHealth * 100;
            float playerHealthPercentage = (float)player.GetComponent<BasicStatsPlayer2>().targetHealth / player.GetComponent<BasicStatsPlayer2>().maxHealth * 100;


            if (shamanHealthPercentage < playerHealthPercentage)
            {
                IncreaseHealth(Healskill);
                print("shaman healed " + targetHealth + "    " + shamanAttributesOS.healthPoint);
                
            }
            else
            {
                player.GetComponent<BasicStatsPlayer2>().IncreaseHealth(Healskill);
                print("player healed " + player.GetComponent<BasicStatsPlayer2>().targetHealth + "     " + player.GetComponent<BasicStatsPlayer2>().playerAttributesOS.healthPoint);
                
            }

            return true;


        }
        return false;
       
       
    }

    public bool Stun()
    {
         if (shamanAttributesOS.mana >= shamanAttributesOS.StunCost)
         {
             DecreaseMana(shamanAttributesOS.StunCost);
             Stunskill = RandomNum(shamanAttributesOS.Stunmin, shamanAttributesOS.Stunmax);
             opponent.GetComponent<BasicStatsEnemy2>().stunTurns += Stunskill;
             return true;
         }
         return false;
        
        
    }

    public bool SHAttack()
    {
         if (shamanAttributesOS.mana >= shamanAttributesOS.SHAttackCost)
         {
             DecreaseMana(shamanAttributesOS.SHAttackCost);
             SHAttackskill = RandomNum(shamanAttributesOS.SHAttackmin, shamanAttributesOS.SHAttackmax);
             opponent.GetComponent<BasicStatsEnemy2>().TakeDamage(SHAttackskill);
             return true;
         }
         return false;
       
        
    }

    public void TakeDamage(int damage)
    {
        targetHealth -= damage;

        targetHealth = Mathf.Clamp(targetHealth, 0, maxHealth);

        if (shamanAttributesOS.healthPoint != targetHealth)
        {
            shamanAttributesOS.healthPoint = targetHealth;
            healthBar.fillAmount = (float)shamanAttributesOS.healthPoint / maxHealth;
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

        if(shamanAttributesOS.mana != targetMana)
        {
            shamanAttributesOS.mana = targetMana;
            manaBar.fillAmount = (float)shamanAttributesOS.mana / maxMana;
        }
    }

    public void IncreaseHealth(int amount)
    {
        targetHealth += amount;

        targetHealth = Mathf.Clamp(targetHealth, 0, maxHealth);

        if (shamanAttributesOS.healthPoint != targetHealth)
        {
            shamanAttributesOS.healthPoint = targetHealth;
            healthBar.fillAmount = (float)shamanAttributesOS.healthPoint / maxHealth;
        }
    }

    public void DecreaseMana(int amount)
    {
        targetMana -= amount;

        targetMana = Mathf.Clamp(targetMana, 0, maxMana);

    }

    public bool Attack()
    {
        throw new NotImplementedException();
    }

    public bool StrongAttack()
    {
        throw new NotImplementedException();
    }

    public bool DeepSharpness()
    {
        throw new NotImplementedException();
    }
}
