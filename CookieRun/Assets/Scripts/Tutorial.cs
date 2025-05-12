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

    private void Awake()
    {
        movement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        if (tuto_1 || tuto_2)
        {
            tutorial.SetActive(true);
        }
        else
        {
            tutorial.SetActive(false);
        }

        speed = movement.speed;
    }

    private void Update()
    {
        if (waitingForInput && tuto_1 && Input.GetKeyDown(KeyCode.Space))
        {
            movement.speed = speed;
            tutoTxt_1.SetActive(false);
            tuto_1 = false;
            waitingForInput = false;
        }

        if (waitingForInput && tuto_2 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            movement.speed = speed;
            tutoTxt_2.SetActive(false);
            tuto_2 = false;
            waitingForInput = false;
        }
    }

    public void TutorialTrigger(string triggername, Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {

            if (triggername == "tuto_1")
            {
                movement.speed = 0;
                tutoTxt_1.SetActive(true);
                waitingForInput = true;
            }
            else if (triggername == "tuto_2")
            {
                movement.speed = 0;
                tutoTxt_2.SetActive(true);
                waitingForInput = true;
            }
        }
    }
}

