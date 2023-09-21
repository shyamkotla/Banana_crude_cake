using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuUI : MonoBehaviour
{

    [SerializeField] Button playButton;
    [SerializeField] Button levelsButton;
    [SerializeField] Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(ClickPlayGame);
        levelsButton.onClick.AddListener(ClickLoadlevelMenu);
        exitButton.onClick.AddListener(ClickExitGame);
    }

    private void ClickPlayGame()
    {
        LevelLoader.instance.LoadSceneIndex(1);
    }
    private void ClickLoadlevelMenu()
    {
        LevelLoader.instance.LoadLevelSelect();
    }
    private void ClickExitGame()
    {
        Application.Quit();
    }
    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(ClickPlayGame);
        levelsButton.onClick.RemoveListener(ClickLoadlevelMenu);
        exitButton.onClick.RemoveListener(ClickExitGame);
    }
}
