using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ScoreType { Current, Best }

public class ShowScore : MonoBehaviour
{
    public Sprite[] numberSprites; // 숫자 스프라이트 목록들
    public GameObject numberPrefab; // 숫자 하나 표시할때 쓰는 프리펩
    public Transform prefabParent; // 프리펩들이 붙을 부모
    
    private List<GameObject> numbers = new List<GameObject>(); // 현재 보여지고 있는 숫자들 리스트
    
    public ScoreType scoreType;
    
    private void Update()
    {
        int score = 0;

        if (scoreType == ScoreType.Current)
            score = GameManager.Instance.StartScore;
        else if (scoreType == ScoreType.Best)
            score = GameManager.Instance.BestScore;

        UpdateScoreDisplay(score);
    }


    public void UpdateScoreDisplay(int score)
    {
        foreach (var obj in numbers)
        {
            Destroy(obj); // obj 객체들 지우기
        }
        numbers.Clear(); // obj 참조들 지우기
        
        string scoreString = score.ToString();

        foreach (char c in scoreString)
        {
            int num = c - '0'; // char 문자 -> 숫자로 변환하는 과정
            
            GameObject number = Instantiate(numberPrefab, prefabParent);
            number.GetComponent<Image>().sprite = numberSprites[num];
            numbers.Add(number);
        }
    }
}
