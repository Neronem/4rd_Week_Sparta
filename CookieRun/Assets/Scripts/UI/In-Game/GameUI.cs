using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText; // 현재 점수
    public TextMeshProUGUI bestScoreText; // 최고 점수
    
    [SerializeField] private Slider healthSlider; // 슬라이더 컴포넌트
    [SerializeField] private float lerpSpeed = 5f; // 부드럽게 움직이는 속도
    
    private PlayerController playerController; // PlayerController 참조
    private float targetHealth; // Lerp에 사용될 플레이어 체력 저장 변수
    
    void Start()
    {
        // PlayerController 스크립트 찾아서 할당
        playerController = FindObjectOfType<PlayerController>();
        healthSlider = transform.Find("HealthBar").GetComponent<Slider>();
        currentScoreText = transform.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>(); 
        bestScoreText = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
        
        healthSlider.maxValue = playerController.playermaxhealth; // 슬라이더의 최대 값 설정
        healthSlider.value = playerController.playermaxhealth;
    }

    void Update()
    {
        // currentScoreText.text = score; 등 구현 필요
        
        targetHealth = playerController.currenthealth; // 프레임마다 플레이어 체력 가져오고
        healthSlider.value = Mathf.Lerp(healthSlider.value, targetHealth, lerpSpeed * Time.deltaTime); // 체력바의 체력 부드럽게 전환
    }

    void GameUIDisappear()
    {
        gameObject.SetActive(false);
    }
}
