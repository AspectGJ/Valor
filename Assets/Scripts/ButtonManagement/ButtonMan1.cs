using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMan1 : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("HealthPotion", 4);
        PlayerPrefs.SetInt("ManaPotion", 6);
    }

    public void StartB()
    {
        //load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitB()
    {
        //quit game
        Application.Quit();
    }
}
