using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTXT;
    [SerializeField] TextMeshProUGUI collectiblesTXT;
    [SerializeField] GameObject playerref;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] float levelTimer = 30f;
    float timer = 0f;
    string timeLeft = "Time Left : ";
    public static GameManager instance;
    bool fired = false;
    int collectibles = 0;
    
    //[SerializeField] Transform maze_entrypoint;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // on spike hit or fall through level
    private void Update()
    {
        //if(levelTimer > 0f)
        //{
        //    levelTimer -= Time.deltaTime;
        //    timerTXT.text = timeLeft + levelTimer.ToString(".0#");
        //}
        //else
        //{
        //    if (!fired)
        //    {
        //        fired = true;
        //        collectiblesTXT.text = "-"+collectibles.ToString();
        //        gameOverCanvas.SetActive(true);
        //    }
        //}
    }

    public void  CoinCollected()
    {
        collectibles++;
        collectiblesTXT.text = collectibles.ToString();
    }


}
