using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trasnport : MonoBehaviour
{
    /*
    public GameObject gate1_2;
    public GameObject gate2_1;
    public GameObject gate2_3;
    public GameObject gate3_2;
    public GameObject gate3_4;
    public GameObject gate4_3;
    */

    public GameObject gate1;
    public GameObject gate2_in;
    public GameObject gate2_out;
    public GameObject gate3_in;
    public GameObject gate3_out;
    public GameObject gate4;
    public GameObject vc1;
    public GameObject vc2;
    public GameObject vc3;
    public GameObject vc4;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    public GameObject cam4;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //if player triggers with gate 1_2, player will be teleported to gate 2_1
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object collided with is one of the gates
        if (other.gameObject.tag == "gate1_2")
        {
            this.gameObject.transform.position = gate2_in.transform.position;
            vc1.SetActive(false);
            cam1.SetActive(false);
            vc2.SetActive(true);
            cam2.SetActive(true);
        }
        else if (other.gameObject.tag == "gate2_1")
        {
            this.gameObject.transform.position = gate1.transform.position;
            vc2.SetActive(false);
            cam2.SetActive(false);
            vc1.SetActive(true);
            cam1.SetActive(true);
        }
        else if (other.gameObject.tag == "gate2_3")
        {
            this.gameObject.transform.position = gate3_in.transform.position;
            vc2.SetActive(false);
            cam2.SetActive(false);
            vc3.SetActive(true);
            cam3.SetActive(true);
        }
        else if (other.gameObject.tag == "gate3_2")
        {
            this.gameObject.transform.position = gate2_out.transform.position;
            vc3.SetActive(false);
            cam3.SetActive(false);
            vc2.SetActive(true);
            cam2.SetActive(true);
        }
        else if (other.gameObject.tag == "gate3_4")
        {
            this.gameObject.transform.position = gate4.transform.position;
            vc3.SetActive(false);
            cam3.SetActive(false);
            vc4.SetActive(true);
            cam4.SetActive(true);
        }
        else if (other.gameObject.tag == "gate4_3")
        {
            this.gameObject.transform.position = gate3_out.transform.position;
            vc4.SetActive(false);
            cam4.SetActive(false);
            vc3.SetActive(true);
            cam3.SetActive(true);
        }
    }
}
