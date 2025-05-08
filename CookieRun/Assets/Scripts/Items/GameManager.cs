// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
//
// public class GameManager : MonoBehaviour
// {
//     [SerializeField] private UIManager uiManager;
//     [SerializeField] private GameObject scoreItemPrefab; 
//     
//     private static GameManager Instance;
//     
//     private int startScore = 0;
//
//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//         }
//     }
//     
//     int itemCount = 0;
//     Vector3 itemLastPosition = Vector3.zero;
//         
//
//     private void Start()
//     {
//         uiManager.UpdateScore(0);
//         
//         // BaseItem[] items = GameObject.FindObjectsOfType<BaseItem>(); 
//         // itemLastPosition = items[0].transform.position;
//         // itemCount = items.Length;
//         //
//         // Debug.Log(itemCount);
//         // Debug.Log(itemLastPosition);
//         //
//         // for (int i = 0; i < itemCount; i++)
//         // {
//         //     //장애물 마지막 위치 = i번째 장애물 위치
//         //     itemLastPosition = items[i].RandomCreate(itemLastPosition);
//         // }
//     }
//     
//     
//     
//     
//     
//
//     #region Score
//
//     public void AddScore(int score)
//     {
//         startScore += score;
//         uiManager.UpdateScore(startScore);
//     }
//     #endregion
//     
// }
