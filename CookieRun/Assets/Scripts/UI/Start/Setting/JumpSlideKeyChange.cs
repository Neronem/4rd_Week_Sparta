using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpSlideKeyChange : MonoBehaviour
{
    public TextMeshProUGUI jumpKeyBindText; // 점프 키바인딩 버튼의 텍스트
    public TextMeshProUGUI slideKeyBindText; // 슬라이드 키바인딩 버튼의 텍스트

    private enum RebindAction
    {
        None,
        Jump,
        Slide
    }; // 어느 키를 바꿀건지 enum으로 판단

    private RebindAction waitingForKey = RebindAction.None; // 설정 안하면 (버튼 안누르면) None

    private void Update()
    {
        if (waitingForKey == RebindAction.None)
        {
            return;
        } // 평소엔 (버튼 안눌렀을땐) return
        
        KeyChanger();
    }

    public void KeyChanger()
    {
        // 버튼 눌렀을때 
        if (Input.anyKeyDown) // 사용자가 키보드의 어떤 거라도 일단 눌렀다면 (없어도 되긴하는데 최적화용)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode))) // 키코드 전체를 순회하며 검사
            {
                if (Input.GetKeyDown(key)) // 이거 눌렀네?
                {
                    switch (waitingForKey) // enum 타입에 따라 바꾸는 키 종류가 다름 (점프, 슬라이드)
                    {
                        case RebindAction.Jump: // 점프 버튼 누른거였으면
                            PlayerInputSettings.jumpKey = key; // 점프키에 키 할당
                            jumpKeyBindText.text = key.ToString(); // 버튼에 텍스트로 키 표시
                            break;
                        case RebindAction.Slide: // 슬라이드 버튼 누른거였으면
                            PlayerInputSettings.slideKey = key; // 슬라이드키에 키 할당
                            slideKeyBindText.text = key.ToString(); // 버튼에 텍스트로 키 표시
                            break;
                    }

                    waitingForKey = RebindAction.None; // 바꾸는거 끝나면 다시 None으로
                    break;
                }
            }
        }
    }
    
    
    public void StartRebindJump()
    {
        waitingForKey = RebindAction.Jump;
        jumpKeyBindText.text = "Press Any Key..";
    }

    public void StartRebindSlide()
    {
        waitingForKey = RebindAction.Slide;
        slideKeyBindText.text = "Press Any Key...";
    }
}

