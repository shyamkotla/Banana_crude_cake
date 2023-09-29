using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] GameObject acidSplashVFx;
    [SerializeField] GameObject levelEndPanel;
    [SerializeField] GameObject levelEndWindow;
    [SerializeField] AudioSource levelmusicTrack;
    [SerializeField] float triggerDelay;
    [SerializeField] Ease easeStyle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var splashpos = collision.GetComponent<Transform>().position;
        SpawnSplashEffect(splashpos);
        levelmusicTrack.Stop();
        //enable level end panel
        levelEndPanel.SetActive(true);
        levelEndWindow.transform.localScale = Vector2.zero;
        levelEndWindow.transform.DOScale(1f, 1f).SetEase(easeStyle).OnComplete(() =>
        {
            //play level complete audio
            SoundManager.instance.PlayLevelCompleteSFx();
            //pause game to disable other inputs
            Debug.Log("gamestopped");
            GameManager.instance.PauseGame();
        });
    }
    private void SpawnSplashEffect(Vector2 splashPos)
    {
        Instantiate(acidSplashVFx, splashPos+new Vector2(0f,-1f), Quaternion.Euler(90f,0f,0f));
        SoundManager.instance.PlayAcidSplashSFx();
    }
}
