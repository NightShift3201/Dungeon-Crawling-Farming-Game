using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance{get;private set;}
    private CinemachineVirtualCamera cinemachine;
    private float shakeTimer;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cinemachine = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time){
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    void Update(){
        if(shakeTimer>0f){
            shakeTimer-=Time.deltaTime;
            if(shakeTimer<=0f){
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
