using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelStart : MonoBehaviour
{
    [SerializeField] float maxForce = 1f;
    [HideInInspector] public UnityEvent LaunchComplete ;
    [SerializeField] PlayerInput playerRB;
    // Start is called before the first frame update
    private void Awake()
    {
        LaunchComplete = new UnityEvent();
        UnityTimer.Timer.Register(0.5f, LaunchSplashSetup);
    }
   
    private void LaunchSplashSetup()
    {
        //start splash VFX and Sfx
        SoundManager.instance.PlayAcidSplashSFx();
        playerRB.playerState = PlayerInput.PlayerState.LAUNCHED;
        playerRB.GetComponent<Rigidbody2D>().AddForce(Vector2.one* maxForce, ForceMode2D.Impulse);
        LaunchComplete.Invoke();
    }
   
}
