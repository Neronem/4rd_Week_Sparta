using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public enum RewardType
    {
        UnlockCustomization, // 커스터마이징 해금
        UnlockParticle, // 파티클 해금
        UnlockPet, // 펫 해금
    }
    public static AchievementManager Instance; // 싱글톤 인스턴스
    [SerializeField] private List<AchievementData> achievementDatas; // 업적 데이터 리스트

    private Dictionary<string, Achievement> achievementDictionary; // 업적 데이터 딕셔너리

    private class Achievement
    {
        public AchievementData data; // 업적 데이터
        public bool isAchieved; // 업적 달성 여부
        public float currentValue; // 현재 값
    }
    [Serializable] public class AchievementReward
    {
        public RewardType rewardType; // 보상 타입
        public int rewardValue; // 보상 값
        public string itemId; // 아이템 ID
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 싱글톤 인스턴스 설정
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }

        achievementDictionary = new Dictionary<string, Achievement>();
        foreach (var data in achievementDatas)
        {
            achievementDictionary[data.achievementId] = new Achievement
            {
                data = data,
                isAchieved = false,
                currentValue = 0
            };
        }
    }
    public bool CheckAchievement(string achievementId) // 업적 달성 체크
    {
        if (achievementDictionary.TryGetValue(achievementId, out var achievement))
        {
            return achievement.isAchieved; // 업적 달성 여부
        }
        return false;
    }

    public void ProgressRate(string achievementId, float progress) // 업적 진행률 업데이트
    {
        if (achievementDictionary.TryGetValue(achievementId, out var achievement))
        {
            achievement.currentValue += progress; // 현재 값 업데이트
            if (achievement.currentValue >= achievement.data.achievementTarget && !achievement.isAchieved)
            {
                achievement.isAchieved = true; // 업적 달성
                UnlockAchievement(achievement.data); // 업적 달성 시 보상 지급
            }
        }
    }

    private void UnlockAchievement(AchievementData data)
    {
        foreach (var reward in data.rewards)
        {
            switch (reward.rewardType)
            {
                case RewardType.UnlockCustomization:
                    SkinManager.Instance.UnlockSkin(reward.itemId); // 커스터마이징 해금 로직
                    break;
                case RewardType.UnlockParticle:
                    // 파티클 해금 로직
                    break;
                case RewardType.UnlockPet:
                    // 펫 해금 로직
                    break;
            }
        }
        Debug.Log($"도전과제 달성 {data.achievementName}"); // 업적 해금 메시지
        //달성 정보 저장 로직 추가 필요
    }
}