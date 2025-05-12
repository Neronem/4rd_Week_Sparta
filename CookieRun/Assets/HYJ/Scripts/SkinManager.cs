using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance; // �̱��� �ν��Ͻ�
    [SerializeField] private List<SkinData> skinDatas; // ��Ų ������ ����Ʈ
    private Dictionary<string, Skin> skinDictionary; // ��Ų ������ ��ųʸ�
    private class Skin
    {
        public SkinData data; // ��Ų ������
        public bool isUnlocked; // ��Ų �ر� ����
    }
    public class SkinData
    {
        public string skinId; // ��Ų ID
        public string skinName; // ��Ų �̸�
        public string skinDescription; // ��Ų ����
        public int price; // ����
        public bool isUnlocked; // �ر� ����
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
    public void UnlockSkin(string skinId) // ��Ų �ر�
    {
        if (skinDictionary.TryGetValue(skinId, out var skin))
        {
            skin.isUnlocked = true; // ��Ų �ر�
            skin.data.isUnlocked = true; // ��Ų ������ ������Ʈ
            // ���� ���� �߰� �ʿ�
        }
        else
        {
            Debug.LogWarning($"Skin with ID {skinId} not found!"); // ��Ų �̹߰� �޽���
        }
    }
    public bool CheckSkinUnlocked(string skinId) // ��Ų �ر� ���� üũ
    {
        if (skinDictionary.TryGetValue(skinId, out var skin))
        {
            return skin.isUnlocked; // ��Ų �ر� ����
        }
        return false;
    }
}
