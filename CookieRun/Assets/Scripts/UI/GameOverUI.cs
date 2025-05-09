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

    private void Start()
    {
        totalScoreText = transform.Find("TotalScoreText").GetComponent<TextMeshProUGUI>();
        bestScoreText = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
        newStageOpenText = transform.Find("NewStageOpenText").GetComponent<TextMeshProUGUI>();
    }
}

