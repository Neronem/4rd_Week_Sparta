using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour
{
    [SerializeField] private string stageSceneName;
    [SerializeField] private int difficultyLevel;
    public void LoadStage()
    {
        // PlayerPrefs.DeleteKey("Stage1Cleared"); stage2 ����ִ��� Ȯ�ο�
        if (difficultyLevel == 2)
        {
            if (PlayerPrefs.GetInt("Stage1Cleared", 0) == 0)
            {
                Debug.Log("Stage 1�� ���� Ŭ�����ؾ� �մϴ�.");
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
}
