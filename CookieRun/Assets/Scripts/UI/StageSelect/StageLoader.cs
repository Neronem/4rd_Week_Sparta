using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour
{
    [SerializeField] private string stageSceneName;
    [SerializeField] private int difficultyLevel;
    public GameObject stagePanel;
    public GameObject playBtn;
    public GameObject stageBtn;

    public void LoadStage()
    {
        GameManager.difficulty = difficultyLevel;
        SceneManager.LoadScene(stageSceneName);
    }
    public void LoadStart()
    {
        SceneManager.LoadScene(stageSceneName);
    }

    public void StaageBtn()
    {
        stageBtn.SetActive(!stageBtn.activeSelf);
        playBtn.SetActive(!playBtn.activeSelf);
        if (stagePanel != null)
            stagePanel.SetActive(!stagePanel.activeSelf);
    }
}
