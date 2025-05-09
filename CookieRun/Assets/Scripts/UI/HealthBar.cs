using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider; // 슬라이더 컴포넌트
    private PlayerController playerController; // PlayerController 참조
    
    [SerializeField] private float lerpSpeed = 5f; // 부드럽게 움직이는 속도
    private float targetHealth; // Lerp에 사용될 플레이어 체력 저장 변수
    
    void Start()
    {
        // PlayerController 스크립트 찾아서 할당
        playerController = FindObjectOfType<PlayerController>();
        healthSlider = GetComponent<Slider>();
        
        healthSlider.maxValue = playerController.playermaxhealth; // 슬라이더의 최대 값 설정
        healthSlider.value = playerController.playermaxhealth;
    }

    void Update()
    {
        targetHealth = playerController.currenthealth; // 프레임마다 플레이어 체력 가져오고
        healthSlider.value = Mathf.Lerp(healthSlider.value, targetHealth, lerpSpeed * Time.deltaTime); // 체력바의 체력 부드럽게 전환
    }
}