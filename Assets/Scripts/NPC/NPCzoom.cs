using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NPCzoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] CinemachineVirtualCamera cam2;
    [SerializeField] CinemachineVirtualCamera cam3;
    [SerializeField] CinemachineVirtualCamera cam4;
    int index;
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
            float targetOrthographicSize = originalOrthographicSize - 0.8f;
            cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        }
        if (index == 2)
        {
            float targetOrthographicSize = originalOrthographicSize - 0.8f;
            cam2.m_Lens.OrthographicSize = Mathf.Lerp(cam2.m_Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        }
        if (index == 4)
        {
            float targetOrthographicSize = originalOrthographicSize - 0.5f;
            cam4.m_Lens.OrthographicSize = Mathf.Lerp(cam4.m_Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        }
        if (index == 5 || index ==3)
        {
            float targetOrthographicSize = originalOrthographicSize - 0.8f;
            cam3.m_Lens.OrthographicSize = Mathf.Lerp(cam3.m_Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        }
        if (index == 0)
        {
            if (!isZoomed)
            {
                float targetOrthographicSize = originalOrthographicSize;
                cam.m_Lens.OrthographicSize = Mathf.Lerp(cam.m_Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
            }

        }
        if (index == -1 )
        {
            if (!isZoomed)
            {
                float targetOrthographicSize = originalOrthographicSize;
                cam2.m_Lens.OrthographicSize = Mathf.Lerp(cam2.m_Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
            }

        }
        if (index == -3)
        {
            if (!isZoomed)
            {
                float targetOrthographicSize = originalOrthographicSize;
                cam4.m_Lens.OrthographicSize = Mathf.Lerp(cam4.m_Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
            }

        }
        if (index == -4 || index == -2)
        {
            if (!isZoomed)
            {
                float targetOrthographicSize = originalOrthographicSize;
                cam3.m_Lens.OrthographicSize = Mathf.Lerp(cam3.m_Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            index=1;
            isZoomed = true;
        }
        if (other.CompareTag("NPC2"))
        {
            index=2;
            isZoomed = true;
        }
        if (other.CompareTag("NPC3"))
        {
            index=3;
            isZoomed = true;
        }
        if (other.CompareTag("NPC4"))
        {
            index=4;
            isZoomed = true;
        }
        if (other.CompareTag("Market"))
        {
            index=5;
            isZoomed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            index=0;
            isZoomed = false;
        }
        if (other.CompareTag("NPC2"))
        {
            index=-1;
            isZoomed = false;
        }
        if (other.CompareTag("NPC3"))
        {
            index=-2;
            isZoomed = false;
        }
        if (other.CompareTag("NPC4"))
        {
            index=-3;
            isZoomed = false;
        }
        if (other.CompareTag("Market"))
        {
            index=-4;
            isZoomed = false;
        }

    }


}
