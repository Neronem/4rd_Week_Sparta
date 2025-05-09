using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //     [SerializeField] private UIManager uiManager;
    //     [SerializeField] private GameObject scoreItemPrefab; 
    //     
    //     private int startScore = 0;
    //

    public static GameManager Instance;

    public int difficulty = 0; //난이도 선언
    public float speed = 0; //속도 선언

    //게임매니저 싱글톤
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        difficulty = 0;

        //난이도에 따른 속도 차이
        switch (difficulty)
        {
            case 0:
                speed = 1;
                break;
            case 1:
                speed = 3;
                break;
            case 2:
                speed = 5;
                break;
        }


        //uiManager.UpdateScore(0);

        // BaseItem[] items = GameObject.FindObjectsOfType<BaseItem>(); 
        // itemLastPosition = items[0].transform.position;
        // itemCount = items.Length;
        //
        // Debug.Log(itemCount);
        // Debug.Log(itemLastPosition);
        //
        // for (int i = 0; i < itemCount; i++)
        // {
        //     //장애물 마지막 위치 = i번째 장애물 위치
        //     itemLastPosition = items[i].RandomCreate(itemLastPosition);
        // }
        //     
        //     int itemCount = 0;
        //     Vector3 itemLastPosition = Vector3.zero;
        //         
        //
    }

    private void Update()
    {
        //시간이 지나며 점차 빨라지는 속도
        speed += Time.deltaTime * 0.1f;
    }
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
}
