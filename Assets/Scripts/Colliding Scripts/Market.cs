using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    [SerializeField] GameObject obj1;
    [SerializeField] GameObject obj2;
    public Button interact2;

    // Start is called before the first frame update
    void Start()
    {
        interact2.GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Market"))
        {
            interact2.GetComponent<Button>().interactable = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other) {
         if (other.CompareTag("Market"))
        {
            interact2.GetComponent<Button>().interactable = false;
        }
    }

    public void panel(){
        obj1.SetActive(false);
        obj2.SetActive(true);
    }

      public void panel2(){
        obj1.SetActive(true);
        obj2.SetActive(false);
    }
}
