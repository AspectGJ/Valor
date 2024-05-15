using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightaudio : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource fsrc;
    void Start()
    {
        fsrc = GetComponent<AudioSource>();
        fsrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
