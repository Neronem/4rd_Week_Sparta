using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public bool isJumping = false;
    public bool isDoubleJumping = false;
    public bool isGrounded = false;
    private Rigidbody2D _rigidbody;
}
