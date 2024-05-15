using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject particle = null;
    [SerializeField] private Vector3 explosionOffset = new Vector3(0,1,0);

    [SerializeField] private CamaraShake cameraShake;
    [SerializeField] private float shakeIntensity = 5;
    [SerializeField] private float shakeTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            cameraShake.ShakeCamera(shakeIntensity,shakeTime);
            GameObject exp = Instantiate(particle,transform.position+explosionOffset,Quaternion.identity);
        }
    }
}
