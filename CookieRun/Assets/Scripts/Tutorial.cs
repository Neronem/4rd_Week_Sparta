using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    PlayerMovement movement;

    public GameObject tutorial;
    public GameObject tutoTxt_1;
    public GameObject tutoTxt_2;

    public bool tuto_1 = true;
    public bool tuto_2 = true;
    bool waitingForInput = false;

    float speed;

    private void Start()
    {
        speed = movement.speed;

        if (tuto_1 || tuto_2)
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
        if (waitingForInput && tuto_1 && Input.GetKey(KeyCode.Space))
        {
            movement.speed = speed;
            tutoTxt_1.SetActive(false);
            tuto_1 = false;
            waitingForInput = false;
        }

        if (waitingForInput && tuto_2 && Input.GetKey(KeyCode.LeftShift))
        {
            movement.speed = speed;
            tutoTxt_2.SetActive(false);
            tuto_2 = false;
            waitingForInput = false;
        }
    }

    public void TutorialTrigger(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (tuto_1)
            {
                movement.speed = 0;
                tutoTxt_1.SetActive(true);
                waitingForInput = true;
            }
            else if (tuto_2)
            {
                movement.speed = 0;
                tutoTxt_2.SetActive(true);
                waitingForInput = true;
            }
        }
    }
}

