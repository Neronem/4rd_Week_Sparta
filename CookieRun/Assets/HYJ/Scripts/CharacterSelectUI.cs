using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] private string skinId;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    private void Start()
    {
        _button.interactable = SkinManager.Instance.CheckSkinUnlocked(skinId);
        SkinManager.Instance.OnSkinUnlocked += SkinUnlocked;
    }
    private void SkinUnlocked(string unlockedSkinId)
    {
        if (unlockedSkinId == skinId)
        {
            gameObject.SetActive(true);
            _button.interactable = true;
        }
    }

    private void OnDestroy()
    {
        SkinManager.Instance.OnSkinUnlocked -= SkinUnlocked;
    }
}