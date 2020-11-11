using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShaker : MonoBehaviour
{
    public static CinemachineShaker Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float timer;
    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                 cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            }
        }
    }

    public void Shake(float intensity) {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
         cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        timer = 0.5f;
    }
}
