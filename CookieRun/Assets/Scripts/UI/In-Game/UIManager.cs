using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 싱글톤 선언
    public static UIManager instance;

    private GameObject canvas;
    private GameObject gameUI;
    private GameObject gameOverUI;
    
    private GameOverUI gameOverUIScript;
    private GameUI gameUIScript;
    
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
        canvas = GameObject.Find("Canvas"); // 캔버스 찾아온 후
        gameUI = canvas.transform.Find("GameUI")?.gameObject; // 캔버스 자식들인 UI들을 찾아옴
        gameOverUI = canvas.transform.Find("GameOverUI")?.gameObject;
        
        gameOverUIScript = gameOverUI.GetComponent<GameOverUI>(); // 각자의 스크립트도 찾아옴
        gameUIScript = gameUI.GetComponent<GameUI>();
        
        gameUI.SetActive(true); 
        gameOverUI.SetActive(false);
    }

    public void GameOverUIAppear()
    {
        gameOverUIScript.GameOverUIAppear();
    }

    public void GameOverUIDisappear()
    {
        gameOverUIScript.GameOverUIDisappear();
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
