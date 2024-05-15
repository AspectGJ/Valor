using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class BasicStatcEnemy : MonoBehaviour
{
    GameObject enemy;
    public AttributesScriptableObject enemyAttributesOS;
    public GameObject opponent;



    public int attack;
    public int vanishAttack;

    public int bleedTurns = 0;

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

        if (opponent == null) // && opponent.gameObject.tag.Equals("Enemy")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

        if (enemyAttributesOS.healthPoint != targetHealth)
        {
            enemyAttributesOS.healthPoint = (int)Mathf.MoveTowards(enemyAttributesOS.healthPoint, targetHealth, healthChangeSpeed * Time.deltaTime);
            healthBar.fillAmount = (float)enemyAttributesOS.healthPoint / maxHealth;
        }

    }

    public void Attack()
    {   
        attack = RandomNum(enemyAttributesOS.enemyAttackmin, enemyAttributesOS.enemyAttackmax);
        opponent.GetComponent<BasicStats>().TakeDamage(attack);
    }

    public void VanishState()
    {
        vanishAttack = RandomNum(enemyAttributesOS.enemyVanishmin, enemyAttributesOS.enemyVanishmax);
        opponent.GetComponent<BasicStats>().TakeDamage(vanishAttack);
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
