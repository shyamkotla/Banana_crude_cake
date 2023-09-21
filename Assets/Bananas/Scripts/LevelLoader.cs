using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneIndex(int n)
    {
        //only to be use for game levels 1 to 6
        SceneManager.LoadScene(n);
    }
    public void LoadMaineMenu()
    {
        SceneManager.LoadScene("Main menu");

    }
    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelection");

    }
}
