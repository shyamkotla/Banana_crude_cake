using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip starCollectibleSfx;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip poundSFx;
    [SerializeField] private AudioClip ghostRespawnSFx;
    [SerializeField] private AudioClip sloMoTimerSFx;
    [SerializeField] private AudioClip flagCheckPointSFx;
    [SerializeField] private AudioClip acidSplashSFx;
    [SerializeField] private AudioClip levelCompleteSFx;
    [SerializeField] private AudioClip[] launchSfx;
    [SerializeField] private AudioClip[] aimStretchSfx;
    [SerializeField] private AudioClip[] firstBounceSfx;
    private AudioSource audioSource;
    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
    }
    public void PlayAimSFx()
    {
        audioSource.clip = aimStretchSfx[Random.Range(0, aimStretchSfx.Length)];
        audioSource.Play();
    }
    public void PlayLaunchSFx()
    {
        audioSource.clip= launchSfx[Random.Range(0,launchSfx.Length)];
        audioSource.Play();
    }
    public void PlayCollectibleSFx()
    {
        audioSource.clip= starCollectibleSfx;
        audioSource.Play();
    }

    public void PlaySloMoTimer()
    {
        audioSource.clip = sloMoTimerSFx;
        audioSource.Play();
    }
    public void PlayBounceSFx()
    {
        audioSource.clip = firstBounceSfx[Random.Range(0,firstBounceSfx.Length)];
        audioSource.Play();
    }
    public void PlayPoundSFx()
    {
        audioSource.clip = poundSFx;
        audioSource.Play();
    }
    public void PlayFlagCheckPointSFx()
    {
        audioSource.clip = flagCheckPointSFx;
        audioSource.Play();
    }
    public void PlayLevelCompleteSFx()
    {
        audioSource.clip = levelCompleteSFx;
        audioSource.Play();
    }
    public void PlayAcidSplashSFx()
    {
        audioSource.clip = acidSplashSFx;
        audioSource.Play();
    }
    public void PlayGhostRespawnSFx(bool value)
    {

        if (value)
        {
            audioSource.clip = ghostRespawnSFx;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
    public void StopAudioSource()
    {
        audioSource.Stop();
    }

}
