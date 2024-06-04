using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public float typingSpeed = 0.1f;  // Time between each letter
    public string fullText = "To be continued";

    private TMP_Text textComponent;
    private string currentText = "";

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void BackToMainMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
