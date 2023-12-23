using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public Button interact;
    public TextMeshProUGUI interactText;
    bool inMap = true;

    // Start is called before the first frame update
    void Start()
    {
        interact.GetComponent<Button>().interactable = false;

        //get current scene number
        int scene = SceneManager.GetActiveScene().buildIndex;

        if(scene != 0)
        {
            inMap = false;  
        }

        if(!inMap)
        {
            //set this script to inactive
            this.enabled = false;
        }
       

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            interact.GetComponent<Button>().interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            interact.GetComponent<Button>().interactable = false;
        }
    }


    public void Interact()
    {
        interactText.text = "You interacted me!!!";
    }


}
