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

    //private void Awake() // gameOverUI는 비활성화 상태로 시작하기 때문에 Awake로 해줘야함
    //{
    //    totalScoreText = transform.Find("TotalScoreText").GetComponent<TextMeshProUGUI>();
    //    bestScoreText = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
    //    newStageOpenText = transform.Find("NewStageOpenText").gameObject;
    //}

    public void GameOverUIAppear()
    {
        totalScoreText.text = GameManager.Instance.StartScore.ToString();
        bestScoreText.text = GameManager.Instance.BestScore.ToString();

        if (GameManager.difficulty == 1 && PlayerPrefs.GetInt("Stage1Cleared", 0) == 1)
        {
            newStageOpenText.SetActive(true);
        }
        else
        {
            newStageOpenText.SetActive(false);
        }
        //// 스테이지 클리어 여부 따져서 텍스트 활성화 여부 판단로직 추가
        //newStageOpenText.SetActive(false);
        
        gameObject.SetActive(true);
    }
}

