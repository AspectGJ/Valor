using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.IO.LowLevel.Unsafe;

public class CamaraShake : MonoBehaviour
{
   private CinemachineVirtualCamera virtualcam;
   private CinemachineBasicMultiChannelPerlin perlin;
    
    private void Awake() {
        virtualcam = GetComponent<CinemachineVirtualCamera>();
        perlin = virtualcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ResetIntensity();
    }

    public void ShakeCamera(float intensity , float shakeTime){
        perlin.m_AmplitudeGain = intensity;
        StartCoroutine(WaitTime(shakeTime));
    }

    IEnumerator WaitTime(float shakeTime){
        yield return new WaitForSeconds(shakeTime);
        ResetIntensity();
    }


    void ResetIntensity(){
        perlin.m_AmplitudeGain = 0f;
    }
}