using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject stageSelectPanel;
    public GameObject settingPanel;
    
    // setting 패널들
    public GameObject characterSettingPanel;
    public GameObject personalSettingPanel;
    
    private GameObject currentPanel;
    
    private void Start()
    { // 시작은 메인메뉴로
        ShowPanel(mainMenuPanel);
    }

    private void Update()
    { // Esc 누를 시 바로 전의 메뉴로 회귀
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentPanel == stageSelectPanel || currentPanel == settingPanel)
            {
                ShowPanel(mainMenuPanel);
            }
            else if (currentPanel == characterSettingPanel || currentPanel == personalSettingPanel)
            {
                ShowPanel(settingPanel);
            }
        }
    }

    // Play 누를 시
    public void OnClickPlay()
    {
        ShowPanel(stageSelectPanel);
    }

    // Setting 누를 시
    public void OnClickSetting()
    {
        ShowPanel(settingPanel);
    }
    
    // Exit 누를 시
    public void OnClickExit()
    {
        Application.Quit();
    }

    // (Setting 패널에서) CharacterSetting 누를 시
    public void OnClickCharacterSetting()
    {
        ShowPanel(characterSettingPanel);
    }

    // (Setting 패널에서) PersonalSetting 누를 시
    public void OnClickPersonalSetting()
    {
        ShowPanel(personalSettingPanel);
    }
    
    private void ShowPanel(GameObject targetPanel)
    { // 선택한 패널만 보여주는 메소드
        mainMenuPanel.SetActive(false);
        stageSelectPanel.SetActive(false);
        settingPanel.SetActive(false);
        characterSettingPanel.SetActive(false);
        personalSettingPanel.SetActive(false);
        
        targetPanel.SetActive(true);
        currentPanel = targetPanel;
    }
}
