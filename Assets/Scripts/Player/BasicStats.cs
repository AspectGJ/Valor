using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using System;
using UnityEngine.UI;


public class BasicStats :  MonoBehaviour, IPersistence
{
    DataPersistenceManager dataPersistenceManager;
    public AttributesScriptableObject playerAttributesOS;
    public GameObject opponent;
    public int attack;
    public int strongAttack;
    public int deepSharpness;

    public int targetHealth;
    int maxHealth;
    

    public int targetMana;
    public int maxMana;
    

    public Image healthBar;
    float healthChangeSpeed = 10f;
    public Image manaBar;
    

   Notifications notifications;
   
    
    void Start()
    {
        dataPersistenceManager = FindObjectOfType<DataPersistenceManager>();
        if(dataPersistenceManager == null)
        {
            Debug.LogError("DataPersistenceManager örneği bulunamadı!");
            return;
        }

        // DataPersistenceManager'dan oyun verilerini yükle
        dataPersistenceManager.LoadGame();
        // Yüklenen verileri kullanarak BasicStats bileşenini güncelle
        LoadData(dataPersistenceManager.data);
    }
    public void LoadData(Data data)
    {
        
        
        if(data == null || data.playerAttributesData == null)
        {
            Debug.LogError("Yüklenen veriler boş veya eksik!");
            return;
        }
        
        playerAttributesOS.healthPoint = data.playerAttributesData.healthPoint;
        targetHealth = playerAttributesOS.healthPoint;
        maxHealth = playerAttributesOS.healthPoint;

        playerAttributesOS.mana = data.playerAttributesData.mana;
        targetMana = playerAttributesOS.mana;
        maxMana = playerAttributesOS.mana;

        playerAttributesOS.attackmin = data.playerAttributesData.attackmin;
        playerAttributesOS.attackmax = data.playerAttributesData.attackmax;

        playerAttributesOS.StrongAttackmin = data.playerAttributesData.StrongAttackmin;
        playerAttributesOS.StrongAttackmax = data.playerAttributesData.StrongAttackmax;

        playerAttributesOS.DeepSharpnessmin = data.playerAttributesData.DeepSharpnessmin;
        playerAttributesOS.DeepSharpnessmax = data.playerAttributesData.DeepSharpnessmax;
    

    }
    public void SaveData(Data data)
    {
       data.playerAttributesData.healthPoint = playerAttributesOS.healthPoint;
        data.playerAttributesData.mana = playerAttributesOS.mana;

       data.playerAttributesData.attackmin = playerAttributesOS.attackmin;
        data.playerAttributesData.attackmax = playerAttributesOS.attackmax;

        data.playerAttributesData.StrongAttackmin = playerAttributesOS.StrongAttackmin;
        data.playerAttributesData.StrongAttackmax = playerAttributesOS.StrongAttackmax;

        data.playerAttributesData.DeepSharpnessmin = playerAttributesOS.DeepSharpnessmin;
        data.playerAttributesData.DeepSharpnessmax = playerAttributesOS.DeepSharpnessmax;
    }

    void Update()
    {
        if (playerAttributesOS.healthPoint <= 0)
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

        if (playerAttributesOS.healthPoint != targetHealth)
        {
            playerAttributesOS.healthPoint = (int)Mathf.MoveTowards(playerAttributesOS.healthPoint, targetHealth, healthChangeSpeed * Time.deltaTime);
            healthBar.fillAmount = (float)playerAttributesOS.healthPoint / maxHealth;
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
            opponent.GetComponent<BasicStatcEnemy>().TakeDamage(attack);

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
            opponent.GetComponent<BasicStatcEnemy>().TakeDamage(strongAttack);

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
            opponent.GetComponent<BasicStatcEnemy>().TakeDamage(deepSharpness);
            opponent.GetComponent<BasicStatcEnemy>().bleedTurns = 3;

            return true;
        }
        return false;
    }

    public void TakeDamage(int damage)
    {
        //playerAttributesOS.healthPoint -= damage;
        targetHealth -= damage;
        
        targetHealth = Mathf.Clamp(targetHealth, 0, maxHealth);

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

    public void IncreaseMana()
    {
        targetMana += 5;

        targetMana = Mathf.Clamp(targetMana, 0, maxMana);
    }

    public void DecreaseMana(int amount)
    {
        targetMana -= amount;

        targetMana = Mathf.Clamp(targetMana, 0, maxMana);

    }

}
