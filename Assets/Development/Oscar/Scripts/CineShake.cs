using Cinemachine;
using UnityEngine;

public class CineShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;  //de cinemamachine camera
    private float shakeTimer;                                   //timer voor het schudden van de camera


    public void ShakeCamera(float _intesity, float _time)
    {
        //krijg het component voor camera shake
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerli = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        //zet de intensiteit van de camera shake
        cinemachineBasicMultiChannelPerli.m_AmplitudeGain = _intesity;

        //update de timer
        shakeTimer = _time;
    }

    //awake wordt opgeroepen zodra het script geladen wordt
    private void Awake()
    {
        //verkrijg de cinemamachine camera
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    //update wordt elke frame opgeroepen
    private void Update()
    {
        //als de shaketimer boven 0 is
        if (shakeTimer > 0)
        {
            //verweider deltatime van de timer
            shakeTimer -= Time.deltaTime;

            //als de shaketimer minder dan of geleik aan 0 is
            if (shakeTimer <= 0f)
            {
                //krijg het component voor camera shake
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerli = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();


                //zet de intensiteit van de camera shake naar 0
                cinemachineBasicMultiChannelPerli.m_AmplitudeGain = 0f;
            }
        }
    }
}