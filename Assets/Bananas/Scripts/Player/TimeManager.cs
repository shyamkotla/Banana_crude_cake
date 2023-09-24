using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    public float sloMoTimeScale = 0.5f;
    [SerializeField] Image timerFillUI;
    [SerializeField] PlayerAnimation playerAnim;
    [SerializeField] Color timeOverColor;
    Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = timerFillUI.color;
    }

    // Update is called once per frame
    void Update()
    {
       
        //if(started)
        //{
        //    //count upto 1sec
        //    sloMoTimer += sloMoTimeScale * Time.unscaledDeltaTime;
        //    if(timerFillUI!= null )
        //    {
        //        timerFillUI.fillAmount -= sloMoTimeScale * Time.unscaledDeltaTime;
        //    }
        //}
        //if (sloMoTimer >= duration)
        //{
        //    playerAnim.SetAimReticle(false);
        //    ResetScales();
        //}
    }
    public void StartTimer()
    {
        SoundManager.instance.PlaySloMoTimer();

        //to avoid physics lag during SloMo
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        //update image fill to red as time runs out
        timerFillUI.DOColor(timeOverColor, duration).SetUpdate(true);

        DOTween.To(() => timerFillUI.fillAmount, x => timerFillUI.fillAmount = x, 0, duration).SetUpdate(true).OnComplete(() => 
        {
            playerAnim.SetAimReticle(false);
            ResetScales(); ;
        });
        
    }
    public void ResetScales()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        timerFillUI.fillAmount = 1f;
        timerFillUI.color = defaultColor;
    }
}
