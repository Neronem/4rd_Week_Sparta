using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    
    private static GameManager Instance;
    
    private int startScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        uiManager.UpdateScore(0);
    }
    
    #region Score

    public void AddScore(int score)
    {
        startScore += score;
        uiManager.UpdateScore(startScore);
    }
    #endregion
    
}
