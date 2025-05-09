using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    GameManager gameManager;

    public GameObject tutorial;
    public GameObject tu_1_Txt;
    public GameObject tu_2_Txt;

    public bool tutorial_1 = true;
    public bool tutorial_2 = true;
    bool waitingForInput = false;

    public float speed;


    private void Start()
    {
        gameManager = GameManager.Instance;

        speed = gameManager.speed;

        if (tutorial_1 || tutorial_2)
        {
            tutorial.SetActive(true);
        }
        else
        {
            tutorial.SetActive(false);
        }
    }

    private void Update()
    {
        if (waitingForInput && tutorial_1 && Input.GetKey(KeyCode.Space))
        {
            gameManager.speed = speed;
            tu_1_Txt.SetActive(false);
            tutorial_1 = false;
            waitingForInput = false;
        }

        if (waitingForInput && tutorial_2 && Input.GetKey(KeyCode.LeftShift))
        {
            gameManager.speed = speed;
            tu_2_Txt.SetActive(false);
            tutorial_2 = false;
            waitingForInput = false;
        }
    }

    public void TutorialTrigger(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (tutorial_1)
            {
                gameManager.speed = 0;
                tu_1_Txt.SetActive(true);
                waitingForInput = true;
            }
            else if (tutorial_2)
            {
                gameManager.speed = 0;
                tu_2_Txt.SetActive(true);
                waitingForInput = true;
            }
        }
    }
}
