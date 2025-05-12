using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour
{
    [SerializeField] private string stageSceneName;
    [SerializeField] private int difficultyLevel;
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject StageSelectPanel;
    [SerializeField] GameObject SettingPanel;

    public void LoadStage()
    {
        if (difficultyLevel == 2)
        {
            if (PlayerPrefs.GetInt("Stage1Cleared", 0) == 0)
            {
                Debug.Log("Stage 1을 먼저 클리어해야 합니다.");
                return;
            }
        }
        GameManager.difficulty = difficultyLevel;
        
        SceneManager.LoadScene(stageSceneName);
    }
    public void LoadStart()
    {
        SceneManager.LoadScene(stageSceneName);
    }

    public void ShowPanel()
    {
        MainMenuPanel.SetActive(false);
        StageSelectPanel.SetActive(true);
        SettingPanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        MainMenuPanel.SetActive(true);
        StageSelectPanel.SetActive(false);
        SettingPanel.SetActive(false);
    }
}
