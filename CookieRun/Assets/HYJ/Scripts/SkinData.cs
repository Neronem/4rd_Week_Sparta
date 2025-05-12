using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObjects/SkinData", order = 1)]
public class SkinData : ScriptableObject
{
    [Tooltip("Unique identifier for this skin")]
    public string skinId;

    [Tooltip("Display name of the skin")]
    public string skinName;

    [Tooltip("Prefab to instantiate for this skin")]
    public GameObject skinPrefab;

    [Tooltip("Unlocked state of the skin")]
    public bool isUnlocked = false;
}