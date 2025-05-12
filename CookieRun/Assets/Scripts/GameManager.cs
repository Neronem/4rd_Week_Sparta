using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int startScore = 0;
    public int StartScore {get {return startScore;}}
    
    // 최고점수 
    private int bestScore = 0;
    public int BestScore { get { return bestScore; } }
    
    public static GameManager Instance;
    
    private string scoreKey = "SavedAndLoadScore";
    
    private bool isGameOver = false; // 게임오버 여부 판단
    
    //게임매니저 싱글톤
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        startScore = 0;
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene("Start");
            }
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        UIManager.instance.GameUIDisappear();
        UIManager.instance.GameOverUIAppear();
    }
}
