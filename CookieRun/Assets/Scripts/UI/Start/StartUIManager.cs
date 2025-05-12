using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject stageSelectPanel;
    public GameObject settingPanel;

    private GameObject currentPanel;
    
    private void Start()
    {
        ShowPanel(mainMenuPanel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentPanel == stageSelectPanel || currentPanel == settingPanel)
            {
                ShowPanel(mainMenuPanel);
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

    private void ShowPanel(GameObject targetPanel)
    {
        mainMenuPanel.SetActive(false);
        stageSelectPanel.SetActive(false);
        settingPanel.SetActive(false);
        
        targetPanel.SetActive(true);
        currentPanel = targetPanel;
    }
}
