using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private List<AchievementData> achievementDatas; // 업적 데이터 리스트

    private Dictionary<string, Achievement> achievementDictionary; // 업적 데이터 딕셔너리

    private class Achievement
    {
        public AchievementData data; // 업적 데이터
        public bool isAchieved; // 업적 달성 여부
        public float currentValue; // 현재 값
    }

    private void Awake()
    {
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
        Debug.Log($"Achievement Unlocked: {data.achievementName}");// 업적 달성 로그
        // 보상 지급 로직 추가
        // 커스터마이징 해금 등
    }
}
