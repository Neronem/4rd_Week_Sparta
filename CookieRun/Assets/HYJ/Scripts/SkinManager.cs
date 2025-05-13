using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance; 
    [SerializeField] private List<SkinData> skinDatas; // ��Ų ������ ����Ʈ
    private Dictionary<string, Skin> skinDictionary; // ��Ų ������ ��ųʸ�
    private string savedSkinId;
    public event Action<string> OnSkinUnlocked; // ��Ų �ر� �̺�Ʈ
    private class Skin
    {
        public SkinData data; // ��Ų ������
        public bool isUnlocked; // ��Ų �ر� ����
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // �̱��� �ν��Ͻ� ����
            DontDestroyOnLoad(gameObject); // DDO
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
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

    public void UnlockSkin(string skinId) // ��Ų �ر�
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
        savedSkinId = skinId; // ������ ��Ų ����
        PlayerPrefs.SetString("SelectedSkin", skinId);
        PlayerPrefs.Save();
    }

    public bool CheckSkinUnlocked(string skinId) // ��Ų �ر� üũ
    {
        if (skinDictionary.TryGetValue(skinId, out var skin))
        {
            return skin.isUnlocked; // ��Ų �ر� ����
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
            Destroy(existing.gameObject); // ���� ��Ų ����
        }

        var go = Instantiate(skin.data.skinPrefab, player.transform);
        go.name = $"Skin_{skinId}"; // ��Ų �̸� ����
    }

    public void OnPlayerSpawn(GameObject player)
    {
        if (string.IsNullOrEmpty(savedSkinId))
        {
            return; // ���õ� ��Ų�� ������ ����
        }
        ApplySkin(player, savedSkinId); // ���õ� ��Ų ����
    }
}
