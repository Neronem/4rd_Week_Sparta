using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText; // 최종 점수
    public TextMeshProUGUI bestScoreText; // 최고 점수
    public TextMeshProUGUI newStageOpenText; // 스테이지 오픈 시 출력되는 텍스트

    private void Awake() // gameOverUI는 비활성화 상태로 시작하기 때문에 Awake로 해줘야함
    {
        totalScoreText = transform.Find("TotalScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreText = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
        newStageOpenText = transform.Find("NewStageOpenText").GetComponent<TextMeshProUGUI>();
    }

    public void GameOverUIAppear()
    {
        totalScoreText.text = GameManager.Instance.StartScore.ToString();
        bestScoreText.text = GameManager.Instance.BestScore.ToString();
     
        Debug.Log("gameOverText is null? " + (totalScoreText == null));
        Debug.Log("retryButton is null? " + (bestScoreText == null));
        
        Debug.Log(totalScoreText.text);
        // 텍스트에 점수 넣고, 스테이지 클리어 여부 따진 뒤
        gameObject.SetActive(true);
    }
}

