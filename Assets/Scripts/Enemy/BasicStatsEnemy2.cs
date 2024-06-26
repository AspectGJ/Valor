using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class BasicStatsEnemy2 : MonoBehaviour
{
    GameObject enemy;
    public AttributesScriptableObject enemyAttributesOS;

    public int attack;
    public int vanishAttack;

    public int bleedTurns = 0;
    public int stunTurns = 0;

    public int targetHealth;
    int maxHealth;


    public Image healthBar;
    float healthChangeSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        targetHealth = enemyAttributesOS.healthPoint;
        maxHealth = enemyAttributesOS.healthPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAttributesOS.healthPoint <= 0)
        {
            Destroy(gameObject);
        }

        

        if (enemyAttributesOS.healthPoint != targetHealth)
        {
            enemyAttributesOS.healthPoint = (int)Mathf.MoveTowards(enemyAttributesOS.healthPoint, targetHealth, healthChangeSpeed * Time.deltaTime);
            healthBar.fillAmount = (float)enemyAttributesOS.healthPoint / maxHealth;
        }

    }

    public void Attack(GameObject opponent)
    {   
        attack = RandomNum(enemyAttributesOS.enemyAttackmin, enemyAttributesOS.enemyAttackmax);
         
        if(opponent.tag.Equals("Player"))
        {
            
            opponent.GetComponent<BasicStatsPlayer2>().TakeDamage(attack); // if the tag is Player
        }
        else
        {
            
            opponent.GetComponent<BasicStatsShaman2>().TakeDamage(attack); // if the tag is Shaman
        }   
    }

    public void VanishState(GameObject opponent)
    {
        vanishAttack = RandomNum(enemyAttributesOS.enemyVanishmin, enemyAttributesOS.enemyVanishmax);

        if(opponent.tag.Equals("Player"))
        {
            
            opponent.GetComponent<BasicStatsPlayer2>().TakeDamage(vanishAttack); // if the tag is Player
        }
        else
        {
            
            opponent.GetComponent<BasicStatsShaman2>().TakeDamage(vanishAttack); // if the tag is Shaman
        }
            
    }

    public void TakeDamage(int damage)
    {
        targetHealth -= damage;

        targetHealth = Mathf.Clamp(targetHealth, 0, maxHealth);
    }

    public int RandomNum(int min, int max)
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(min, max);
        return randomNumber;
    }
}
