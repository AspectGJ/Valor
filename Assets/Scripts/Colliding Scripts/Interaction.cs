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

    public GameObject adMan;


    [SerializeField] GameObject obj1;
    [SerializeField] GameObject obj2;
    [SerializeField] GameObject obj3;

    [SerializeField] TMP_Text dialogText;
    public string[] dialog;
    public int index;
    public float wordSpeed;

    // Start is called before the first frame update
    void Start()
    {
        interact.GetComponent<Button>().interactable = false;

        //get current scene number
        int scene = SceneManager.GetActiveScene().buildIndex;

        if(scene != 1)
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

    private  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            interact.GetComponent<Button>().interactable = true;
        }

        if (collision.gameObject.tag == "Dungeon")
        {
            //load ad
            adMan.GetComponent<InterstitialAdExample>().ShowAd();

            //load next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            interact.GetComponent<Button>().interactable = false;
        }
        
    }

public void OnButtonClicked()
    {
        // Burada her bir duruma göre farklı metotları çağırabilirsiniz
        if (obj1.activeSelf)
        {
            Interact();
        }
        else if (obj3.activeSelf)
        {
            panel();
        }
    }
    public void Interact()
    {
        //interactText.text = "Kill the Beast in dungeon!!";
        obj1.SetActive(false);
        obj2.SetActive(true);
        StartCoroutine(Typing());
    }

    public void panel(){
        obj1.SetActive(false);
        obj3.SetActive(true);
    }

    public void zeroText(){
        dialogText.text = "";
        index = 0;
        obj2.SetActive(false);
    }

    IEnumerator Typing(){
        foreach(char letter in dialog[index].ToCharArray()){
            dialogText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        Debug.Log(dialog[index].ToCharArray().Length);
        if( dialogText.text.Length == dialog[index].ToCharArray().Length){
            yield return  new WaitForSeconds(3);
            obj1.SetActive(true);
            obj2.SetActive(false);
            zeroText();
        }

    }


}
