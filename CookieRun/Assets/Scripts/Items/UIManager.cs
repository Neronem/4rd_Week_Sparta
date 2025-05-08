using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void UpdateScore(int score)
    {
        Debug.Log($"score가 들어왔습니다. {score}");
    }
}
