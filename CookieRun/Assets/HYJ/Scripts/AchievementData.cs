using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementData", menuName = "ScriptableObjects/AchievementData", order = 1)]
public class AchievementData : ScriptableObject
{
    public string achievementId; // ���� �ĺ���
    public string achievementName; // ���� �̸�
    public string achievementDescription; // ���� ����
    public float achievementTarget; // ���� ��ǥ ��
    public int reward; // ���� ����
    public Sprite achievemetIcon; // ���� ������
}