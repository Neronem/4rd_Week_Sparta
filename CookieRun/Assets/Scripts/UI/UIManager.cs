using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Start,
    Game,
    GameOver,
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    UIState currentState = UIState.Start;

    private void Awake()
    {
        if (instance == null)
        {
            instance  = this;
        }
    }

    public void UpdateScore(int score)
    {
        Debug.Log($"score가 들어왔습니다. {score}");
    }
}
