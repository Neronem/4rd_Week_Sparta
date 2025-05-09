using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private List<AchievementData> achievementDatas; // ���� ������ ����Ʈ

    private Dictionary<string, Achievement> achievementDictionary; // ���� ������ ��ųʸ�

    private class Achievement
    {
        public AchievementData data; // ���� ������
        public bool isAchieved; // ���� �޼� ����
        public float currentValue; // ���� ��
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
    public bool CheckAchievement(string achievementId) // ���� �޼� üũ
    {
        if (achievementDictionary.TryGetValue(achievementId, out var achievement))
        {
            return achievement.isAchieved; // ���� �޼� ����
        }
        return false;
    }

    public void ProgressRate(string achievementId, float progress) // ���� ����� ������Ʈ
    {
        if (achievementDictionary.TryGetValue(achievementId, out var achievement))
        {
            achievement.currentValue += progress; // ���� �� ������Ʈ
            if (achievement.currentValue >= achievement.data.achievementTarget && !achievement.isAchieved)
            {
                achievement.isAchieved = true; // ���� �޼�
                UnlockAchievement(achievement.data); // ���� �޼� �� ���� ����
            }
        }
    }

    private void UnlockAchievement(AchievementData data)
    {
        Debug.Log($"Achievement Unlocked: {data.achievementName}");// ���� �޼� �α�
        // ���� ���� ���� �߰�
        // Ŀ���͸���¡ �ر� ��
    }
}
