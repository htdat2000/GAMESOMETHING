using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineScreenShake : MonoBehaviour
{
    private CinemachineVirtualCamera cVCam;
    private CinemachineBasicMultiChannelPerlin cBMP;
    private float shakeTimer;
    float startingIntensity;
    float shakeTimerTotal;
    float startingFrequency;
    private void Awake() {
        cVCam = GetComponent<CinemachineVirtualCamera>();
        cBMP = cVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        cBMP.m_AmplitudeGain = intensity;
        shakeTimer = time;
        startingIntensity = intensity;
        shakeTimerTotal = time;
    }
    public void PlayerGetHit()
    {
        cBMP.m_AmplitudeGain = 3;
        cBMP.m_FrequencyGain = 5;
        shakeTimer = 0.2f;
        startingIntensity = 3;
        startingFrequency = 5;
        shakeTimerTotal = 0.2f;
    }
    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.time;
            if(shakeTimer <= 0f)
            {
                cBMP.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 1f, 1 - shakeTimer/shakeTimerTotal);
                cBMP.m_FrequencyGain = Mathf.Lerp(startingFrequency, 0.01f, 1 - shakeTimer/shakeTimerTotal);
            }
        }
    }
}
