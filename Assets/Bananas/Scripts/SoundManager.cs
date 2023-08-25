using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip launchSfx;
    [SerializeField] private AudioClip starCollectibleSfx;
    [SerializeField] private AudioClip gameMusic;
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

    public void PlayLaunchSFx()
    {
        audioSource.clip= launchSfx;
        audioSource.Play();
    }
    public void PlayCollectibleSFx()
    {
        audioSource.clip= starCollectibleSfx;
        audioSource.Play();
    }
}
