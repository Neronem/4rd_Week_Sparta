using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    // 싱글톤 선언
    public static GameUIManager instance;
    
    public GameObject gameUI;
    public GameObject gameOverUI;
    
    public GameOverUI gameOverUIScript;
    public GameUI gameUIScript;
    
    private void Awake()
    {
        // 싱글톤패턴
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        gameUI.SetActive(true); 
        gameOverUI.SetActive(false);
    }

    public void GameOverUIAppear()
    {
        gameOverUIScript.GameOverUIAppear();
    }

    public void GameUIDisappear()
    {
        gameUIScript.GameUIDisappear();
    }
    
    // "Exit" 선택 시 게임중단 메소드 (버튼용)
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    public void UpdateScore(int score)
    {
        Debug.Log($"score가 들어왔습니다. {score}");
    }
}
