using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class Interaction : MonoBehaviour
{
    public GameObject shaman;
    public Button interact;
    public TextMeshProUGUI interactText;
    bool inMap = true;

    public GameObject adMan;


    [SerializeField] GameObject obj1;
    [SerializeField] GameObject obj2;
    [SerializeField] GameObject obj3;
    [SerializeField] GameObject obj4;
    [SerializeField] GameObject obj5;
    [SerializeField] GameObject obj6;
    [SerializeField] GameObject obj7;

    [SerializeField] GameObject obj8; // Danger sign For NPC villager
    [SerializeField] GameObject obj9; // Danger sign For NPC in town 
    [SerializeField] GameObject obj10; // Danger sign For Shaman NPC

    [SerializeField] GameObject obj11; // Danger sign For NPC Villager in stage 1


    [SerializeField] TMP_Text dialogText;

    public string[] dialog;
    public string[] dialog2;
    public string[] dialog3;
    public string[] dialog4;
    public int index, k = 0, l = 0, m = 0, n = 0;
    public float wordSpeed;

    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        interact.GetComponent<Button>().interactable = false;

        //get current scene number
        int scene = SceneManager.GetActiveScene().buildIndex;

        if (scene != 1)
        {
            inMap = false;
        }

        if (!inMap)
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
            k = 1;
        }
        if (collision.gameObject.CompareTag("NPC2"))
        {
            interact.GetComponent<Button>().interactable = true;
            l = 1;
        }
        if (collision.gameObject.CompareTag("NPC3"))
        {
            interact.GetComponent<Button>().interactable = true;
            m = 1;
        }
        if (collision.gameObject.CompareTag("NPC4"))
        {
            interact.GetComponent<Button>().interactable = true;
            n = 1;
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
            k = 0;
        }
        if (collision.gameObject.CompareTag("NPC2"))
        {
            interact.GetComponent<Button>().interactable = false;
            l = 0;
        }
        if (collision.gameObject.CompareTag("NPC3"))
        {
            interact.GetComponent<Button>().interactable = false;
            m = 0;
        }
        if (collision.gameObject.CompareTag("NPC4"))
        {
            interact.GetComponent<Button>().interactable = false;
            shaman.SetActive(true);
            // set shaman position to player's position (but make x - 3)
            shaman.transform.position = new Vector3(collision.transform.position.x - 3, collision.transform.position.y, collision.transform.position.z);
            Destroy(collision.gameObject);
            n = 0;
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
        if (k == 1)
        {
            obj1.SetActive(false);
            obj2.SetActive(true);
            StartCoroutine(Typing());
            obj11.SetActive(false);
        }

        if (l == 1)
        {
            obj1.SetActive(false);
            obj2.SetActive(true);
            StartCoroutine(Typing2());
            obj4.SetActive(false);
            obj6.SetActive(false);
            obj7.SetActive(false);
            obj5.SetActive(true);
            obj8.SetActive(false);
        }
        if (m == 1)
        {
            obj1.SetActive(false);
            obj2.SetActive(true);
            StartCoroutine(Typing3());
            obj4.SetActive(false);
            obj6.SetActive(true);
            obj7.SetActive(false);
            obj5.SetActive(false);
            obj9.SetActive(false);
       
        }

        if (n == 1)
        {
            obj1.SetActive(false);
            obj2.SetActive(true);
            StartCoroutine(Typing4());
            obj4.SetActive(false);
            obj6.SetActive(false);
            obj7.SetActive(true);
            obj5.SetActive(false);
            obj10.SetActive(false);
        }
    }

    public void panel()
    {
        obj1.SetActive(false);
        obj3.SetActive(true);
    }

    public void zeroText()
    {
        dialogText.text = "";
        index = 0;
        obj2.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialog[index].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        Debug.Log(dialog[index].ToCharArray().Length);
        if (dialogText.text.Length == dialog[index].ToCharArray().Length)
        {
            yield return new WaitForSeconds(3);
            obj1.SetActive(true);
            obj2.SetActive(false);
            zeroText();
        }

    }
    IEnumerator Typing2()
    {
        foreach (char letter2 in dialog2[index].ToCharArray())
        {
            dialogText.text += letter2;
            yield return new WaitForSeconds(wordSpeed);
        }
        Debug.Log(dialog2[index].ToCharArray().Length);
        if (dialogText.text.Length == dialog2[index].ToCharArray().Length)
        {
            yield return new WaitForSeconds(3);
            obj1.SetActive(true);
            obj2.SetActive(false);
            zeroText();
        }

    }
    IEnumerator Typing3()
    {
        foreach (char letter3 in dialog3[index].ToCharArray())
        {
            dialogText.text += letter3;
            yield return new WaitForSeconds(wordSpeed);
        }
        Debug.Log(dialog3[index].ToCharArray().Length);
        if (dialogText.text.Length == dialog3[index].ToCharArray().Length)
        {
            yield return new WaitForSeconds(3);
            obj1.SetActive(true);
            obj2.SetActive(false);
            zeroText();
        }

    }

    IEnumerator Typing4()
    {
        foreach (char letter4 in dialog4[index].ToCharArray())
        {
            dialogText.text += letter4;
            yield return new WaitForSeconds(wordSpeed);
        }
        //Debug.Log(dialog4[index].ToCharArray().Length);
        if (dialogText.text.Length == dialog4[index].ToCharArray().Length)
        {
            yield return new WaitForSeconds(3);
            obj1.SetActive(true);
            obj2.SetActive(false);
            zeroText();
        }

    }


}
