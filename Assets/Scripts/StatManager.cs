using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatManager : MonoBehaviour
{
    
    public TextMeshProUGUI playerHP;
    public TextMeshProUGUI playerMana;

    public TextMeshProUGUI enemyHP;
    public TextMeshProUGUI enemyBleedTurns;

    public GameObject player;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get the player's hp
        
        //if the meshes are not null, then get the hp of the player and enemy
        if (player != null && enemy != null)
        {           
            playerHP.text = player.GetComponent<BasicStats>().playerAttributesOS.healthPoint.ToString();        
            playerMana.text = player.GetComponent<BasicStats>().playerAttributesOS.mana.ToString();

            enemyHP.text = enemy.GetComponent<BasicStatcEnemy>().enemyAttributesOS.healthPoint.ToString();
            enemyBleedTurns.text = enemy.GetComponent<BasicStatcEnemy>().bleedTurns.ToString();
        }
        
    }
}
