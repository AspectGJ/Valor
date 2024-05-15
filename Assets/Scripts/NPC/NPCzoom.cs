using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NPCzoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    int index = 0;
    bool isZoomed = true;
    float zoomSpeed = 1f;

    private float originalOrthographicSize;
    // Start is called before the first frame update
    void Start()
    {
        originalOrthographicSize = cam.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (index == 1)
        {
            float targetOrthographicSize = originalOrthographicSize - 1.2f;
            cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        }
        else if (index == 0)
        {
            if (!isZoomed)
            {
                float targetOrthographicSize = originalOrthographicSize;
                cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            index++;
            isZoomed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            index--;
            isZoomed = false;
        }

    }


}
