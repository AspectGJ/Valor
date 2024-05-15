using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BasicStatsShaman2 : MonoBehaviour
{
    public AttributesScriptableObject shamanAttributesOS;
    public GameObject opponent;
    public int firstSkill;
    public int secondSkill;
    public int thirdSkill;

    public int targetHealth;
    int maxHealth;

    public int targetMana;
    public int maxMana;


    public Image healthBar;
    float healthChangeSpeed = 10f;
    public Image manaBar;


    Notifications notifications;

    public void LoadData(Data data)
    {
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
        Debug.Log("Target HP: " + targetHealth + " Max HP: " + maxHealth + "Current Health: " + shamanAttributesOS.healthPoint);
    }

    public void SaveData(Data data)
    {
        data.shamanAttributesData.healthPoint = shamanAttributesOS.healthPoint;
        data.shamanAttributesData.mana = shamanAttributesOS.mana;

        data.shamanAttributesData.attackmin = shamanAttributesOS.attackmin;
        data.shamanAttributesData.attackmax = shamanAttributesOS.attackmax;

        data.shamanAttributesData.StrongAttackmin = shamanAttributesOS.StrongAttackmin;
        data.shamanAttributesData.StrongAttackmax = shamanAttributesOS.StrongAttackmax;

        data.shamanAttributesData.DeepSharpnessmin = shamanAttributesOS.DeepSharpnessmin;
        data.shamanAttributesData.DeepSharpnessmax = shamanAttributesOS.DeepSharpnessmax;
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

        if (shamanAttributesOS.healthPoint != targetHealth)
        {
            shamanAttributesOS.healthPoint = (int)Mathf.MoveTowards(shamanAttributesOS.healthPoint, targetHealth, healthChangeSpeed * Time.deltaTime);
            healthBar.fillAmount = (float)shamanAttributesOS.healthPoint / maxHealth;
        }
        if (shamanAttributesOS.mana != targetMana)
        {
            shamanAttributesOS.mana = (int)Mathf.MoveTowards(shamanAttributesOS.mana, targetMana, healthChangeSpeed * Time.deltaTime);
            manaBar.fillAmount = (float)shamanAttributesOS.mana / maxMana;
        }
    }

    public bool FirstSkill()
    {
       // if (shamanAttributesOS.mana >= shamanAttributesOS.attackCost)
       // {
       //     DecreaseMana(shamanAttributesOS.attackCost);
       //     firstSkill = RandomNum(shamanAttributesOS.attackmin, shamanAttributesOS.attackmax);
       //     opponent.GetComponent<BasicStatcEnemy>().TakeDamage(firstSkill);
       //     return true;
       // }
       // return false;
       
       return true;
    }

    public bool SecondSkill()
    {
        // if (shamanAttributesOS.mana >= shamanAttributesOS.attackCost)
        // {
        //     DecreaseMana(shamanAttributesOS.attackCost);
        //     secondSkill = RandomNum(shamanAttributesOS.StrongAttackmin, shamanAttributesOS.StrongAttackmax);
        //     opponent.GetComponent<BasicStatcEnemy>().TakeDamage(secondSkill);
        //     return true;
        // }
        // return false;
        
        return true;
    }

    public bool ThirdSkill()
    {
        // if (shamanAttributesOS.mana >= shamanAttributesOS.attackCost)
        // {
        //     DecreaseMana(shamanAttributesOS.attackCost);
        //     thirdSkill = RandomNum(shamanAttributesOS.DeepSharpnessmin, shamanAttributesOS.DeepSharpnessmax);
        //     opponent.GetComponent<BasicStatcEnemy>().TakeDamage(thirdSkill);
        //     return true;
        // }
        // return false;
       
        return true;
    }

    public void TakeDamage(int damage)
    {
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
