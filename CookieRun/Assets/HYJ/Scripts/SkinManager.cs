using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance; 
    [SerializeField] private List<SkinData> skinDatas; // 스킨 데이터 리스트
    private Dictionary<string, Skin> skinDictionary; // 스킨 데이터 딕셔너리
    private string savedSkinId;
    public event Action<string> OnSkinUnlocked; // 스킨 해금 이벤트
    private class Skin
    {
        public SkinData data; // 스킨 데이터
        public bool isUnlocked; // 스킨 해금 여부
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
        skinDictionary = skinDatas.ToDictionary(
            d => d.skinId,
            d => new Skin
            {
                data = d,
                isUnlocked = PlayerPrefs.GetInt($"Skin_{d.skinId}_Unlocked", d.isUnlocked ? 1 : 0) == 1
            }
        );
        foreach (var kv in skinDictionary)
            kv.Value.data.isUnlocked = kv.Value.isUnlocked;

        savedSkinId = PlayerPrefs.GetString("SelectedSkin", null);
    }

    public void UnlockSkin(string skinId) // 스킨 해금
    {
        if (skinDictionary.TryGetValue(skinId, out var skin) && !skin.isUnlocked)
        {
            skin.isUnlocked = true;
            PlayerPrefs.SetInt($"Skin_{skinId}_Unlocked", 1);
            PlayerPrefs.Save();
            OnSkinUnlocked?.Invoke(skinId);
        }
    }

    public void SelectSkin(string skinId)
    {
        if (!skinDictionary.TryGetValue(skinId, out var skin) || !skin.isUnlocked)
        {
            return;
        }
        savedSkinId = skinId; // 선택한 스킨 저장
        PlayerPrefs.SetString("SelectedSkin", skinId);
        PlayerPrefs.Save();
    }

    public bool CheckSkinUnlocked(string skinId) // 스킨 해금 체크
    {
        if (skinDictionary.TryGetValue(skinId, out var skin))
        {
            return skin.isUnlocked; // 스킨 해금 여부
        }
        return false;
    }

    private void ApplySkin(GameObject player, string skinId)
    {
        if ((!skinDictionary.TryGetValue(skinId, out var skin) || !skin.isUnlocked || skin.data.skinPrefab == null))
        {
            return;
        }
        var existing = player.transform.Find($"Skin_{skinId}");
        if (existing != null)
        {
            Destroy(existing.gameObject); // 기존 스킨 제거
        }

        var go = Instantiate(skin.data.skinPrefab, player.transform);
        go.name = $"Skin_{skinId}"; // 스킨 이름 설정
    }

    public void OnPlayerSpawn(GameObject player)
    {
        if (string.IsNullOrEmpty(savedSkinId))
        {
            return; // 선택된 스킨이 없으면 종료
        }
        ApplySkin(player, savedSkinId); // 선택된 스킨 적용
    }
}
