using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject playerref;
     public static GameManager instance;
    



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
    public void SpawnAtLastCheckPoint()
    {
        
    }

    void SpinPlayer()
    {

    }

    
}
