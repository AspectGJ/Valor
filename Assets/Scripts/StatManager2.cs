using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatManager2 : MonoBehaviour
{
    
    public TextMeshProUGUI playerHP;
    public TextMeshProUGUI playerMana;

    public TextMeshProUGUI shamanHp;
    public TextMeshProUGUI shamanMana;

    public TextMeshProUGUI enemyHP;
    public TextMeshProUGUI enemyBleedTurns;

    public GameObject[] players;
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
        if (players.Length != 0 && enemy != null)
        {
            foreach(GameObject player in players)
            {
                if(player.tag == "Player")
                {
                    playerHP.text = player.GetComponent<BasicStatsPlayer2>().playerAttributesOS.healthPoint.ToString();           
                    playerMana.text = player.GetComponent<BasicStatsPlayer2>().playerAttributesOS.mana.ToString();
                }
                else if(player.tag == "Shaman")
                {
                    shamanHp.text = player.GetComponent<BasicStatsShaman2>().shamanAttributesOS.healthPoint.ToString();
                    shamanMana.text = player.GetComponent<BasicStatsShaman2>().shamanAttributesOS.mana.ToString();
                }
            }

            enemyHP.text = enemy.GetComponent<BasicStatsEnemy2>().enemyAttributesOS.healthPoint.ToString();
            enemyBleedTurns.text = enemy.GetComponent<BasicStatsEnemy2>().bleedTurns.ToString();
        }
        
    }
}
