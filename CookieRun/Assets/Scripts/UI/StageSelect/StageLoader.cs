using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour
{
    [SerializeField] private string stageSceneName;
    [SerializeField] private int difficultyLevel;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject stageSelectPanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject personalSettingPanel;
    [SerializeField] GameObject characterSelectPanel;
    public void LoadStage()
    {
        // PlayerPrefs.DeleteKey("Stage1Cleared"); stage2 ����ִ��� Ȯ�ο�
        if (difficultyLevel == 2)
        {
            if (PlayerPrefs.GetInt("Stage1Cleared", 0) == 0)
            {
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
    public void Back()
    {
        stageSelectPanel.SetActive(false); 
        settingPanel.SetActive(false); 
        personalSettingPanel.SetActive(false); 
        characterSelectPanel.SetActive(false); 
        mainMenuPanel.SetActive(true);
    }
}
