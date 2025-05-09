using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject tu_1_Txt;
    public GameObject tu_2_Txt;

    public bool tutorial_1 = true;
    public bool tutorial_2 = true;


    private void Start()
    {
        if (tutorial_1 || tutorial_2)
        {
            tutorial.SetActive(true);
        }
        else
        {
            tutorial.SetActive(false);
        }
    }

    public void TutorialTrigger(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (tutorial_1)
            {
                //player speed = 0
                tu_1_Txt.SetActive(true);
                if (Input.GetKey(KeyCode.Space))
                {
                    //player speed = 1
                    tu_1_Txt.SetActive(false);
                    tutorial_1 = false;
                }
            }

            if (tutorial_2)
            {
                //player speed = 0
                tu_2_Txt.SetActive(true);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    //player speed = 1
                    tu_2_Txt.SetActive(false);
                    tutorial_2 = false;
                }
            }
        }
    }
}
