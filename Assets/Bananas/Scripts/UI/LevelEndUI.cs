using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelEndUI : MonoBehaviour
{
    [SerializeField] Button levelsButton;
    [SerializeField] Button mainMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        levelsButton.onClick.AddListener(ClickLoadlevelMenu);
        mainMenuButton.onClick.AddListener(ClickBacktoMainMenu);
    }

    private void ClickLoadlevelMenu()
    {
        GameManager.instance.PauseGame();
        LevelLoader.instance.LoadLevelSelect();
    }
    private void ClickBacktoMainMenu()
    {
        GameManager.instance.PauseGame();
        LevelLoader.instance.LoadMaineMenu();
    }
    private void OnDestroy()
    {
        levelsButton.onClick.RemoveListener(ClickLoadlevelMenu);
        mainMenuButton.onClick.RemoveListener(ClickBacktoMainMenu);
    }
}
