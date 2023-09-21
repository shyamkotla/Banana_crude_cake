using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField] Button backButton;
    [SerializeField] Button lv1Button;
    [SerializeField] Button lv2Button;

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(ClickBacktoMainMenu);
        lv1Button.onClick.AddListener(() => { 
            ClickLoadLevel(1); 
        });
        lv2Button.onClick.AddListener(() => {
            ClickLoadLevel(2);
        });
    }

    private void ClickBacktoMainMenu()
    {
        LevelLoader.instance.LoadMaineMenu();
    }
    public void ClickLoadLevel(int num)
    {
        LevelLoader.instance.LoadSceneIndex(num);
    }
    private void OnDestroy()
    {
        backButton.onClick.RemoveListener(ClickBacktoMainMenu);
        lv1Button.onClick.RemoveAllListeners();
        lv2Button.onClick.RemoveAllListeners();
    }
}
