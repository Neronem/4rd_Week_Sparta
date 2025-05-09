using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
// 현재 UI 상태 Enum으로 판단    
public enum UIState
{ 
    Start,
    Game,
    GameOver,
}

public class UIManager : MonoBehaviour
{
    // 싱글톤 선언
    public static UIManager instance;
    
    // 기본 UI 상태 = 시작
    UIState currentState = UIState.Start;
    
    // 각 UI들 미리 선언
    StartUI startUI = null;
    GameUI gameUI = null;
    GameOverUI gameOverUI = null;
    
    private void Awake()
    {
        // 싱글톤패턴
        if (instance == null)
        {
            instance  = this;
        }
        
        // UIManager 각 UI에 할당
        startUI = GetComponentInChildren<StartUI>(true);
        startUI?.Init(this);
        
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);
        
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI?.Init(this);
    }
    
    // UI상태 바꾸는 메소드. 자세한 로직은 BaseUI의 SetActive 참조
    public void ChangeState(UIState state)
    {
        currentState = state;
        startUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        gameOverUI?.SetActive(currentState);
    }

    // "Play" 선택 시 게임진입 메소드 (버튼용)
    public void OnClickStart()
    {
        ChangeState(UIState.Game);
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
