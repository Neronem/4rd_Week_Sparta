using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance; // 싱글톤 인스턴스
    [SerializeField] private List<SkinData> skinDatas; // 스킨 데이터 리스트
    private Dictionary<string, Skin> skinDictionary; // 스킨 데이터 딕셔너리
    private class Skin
    {
        public SkinData data; // 스킨 데이터
        public bool isUnlocked; // 스킨 해금 여부
    }
    public class SkinData
    {
        public string skinId; // 스킨 ID
        public string skinName; // 스킨 이름
        public string skinDescription; // 스킨 설명
        public int price; // 가격
        public bool isUnlocked; // 해금 여부
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 싱글톤 인스턴스 설정
            DontDestroyOnLoad(gameObject); // DDO
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 제거
            return;
        }
        skinDictionary = new Dictionary<string, Skin>();
        foreach (var data in skinDatas)
        {
            skinDictionary[data.skinId] = new Skin
            {
                data = data,
                isUnlocked = data.isUnlocked
            };
        }
    }
    public void UnlockSkin(string skinId) // 스킨 해금
    {
        if (skinDictionary.TryGetValue(skinId, out var skin))
        {
            skin.isUnlocked = true; // 스킨 해금
            skin.data.isUnlocked = true; // 스킨 데이터 업데이트
            // 저장 로직 추가 필요
        }
        else
        {
            Debug.LogWarning($"Skin with ID {skinId} not found!"); // 스킨 미발견 메시지
        }
    }
    public bool CheckSkinUnlocked(string skinId) // 스킨 해금 여부 체크
    {
        if (skinDictionary.TryGetValue(skinId, out var skin))
        {
            return skin.isUnlocked; // 스킨 해금 여부
        }
        return false;
    }
}
