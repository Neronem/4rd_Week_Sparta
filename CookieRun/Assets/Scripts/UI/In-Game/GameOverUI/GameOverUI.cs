using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText; // 최종 점수
    public TextMeshProUGUI bestScoreText; // 최고 점수
    public GameObject newStageOpenText; // 스테이지 오픈 시 출력되는 텍스트

    public void GameOverUIAppear()
    {
        totalScoreText.text = GameManager.Instance.StartScore.ToString();
        bestScoreText.text = GameManager.Instance.BestScore.ToString();
        
        // 스테이지 클리어 여부 따져서 텍스트 활성화 여부 판단로직 추가
        newStageOpenText.SetActive(false);
        
        gameObject.SetActive(true);
    }
}

