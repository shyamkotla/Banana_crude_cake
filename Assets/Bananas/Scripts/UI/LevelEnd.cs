using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] GameObject levelEndPanel;
    [SerializeField] GameObject levelEndWindow;
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
        //enable level end panel
        levelEndPanel.SetActive(true);
        levelEndWindow.transform.localScale = Vector2.zero;
        levelEndWindow.transform.DOScale(1f, 1f).SetEase(easeStyle).OnComplete(() =>
        {
            //pause game to disable other inputs
            Debug.Log("gamestopped");
            GameManager.instance.PauseGame();
            
        });


    }
}
