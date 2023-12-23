using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    //public float offset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //make the camera follow the player's position without z axis with a little offset
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z) + new Vector3(1, 1f, 0);

        
    }
}
