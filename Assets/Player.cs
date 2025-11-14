using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float xInput;

    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpHeight = 8;
    [SerializeField] private float DashPower = 2;

    [Header("Collision details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool isTouchingGround;
    [SerializeField] private LayerMask ground;

    [SerializeField] private bool canDoubleJump;

    

    void Start()
    {
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        handleCollisions();
        Movement();
    }


    private void Movement()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            jumpAttempt();
        }

        if ((isTouchingGround && !Input.GetKeyDown(KeyCode.Space)) && !Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            canDoubleJump = !canDoubleJump;
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            dash();
        }

        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);

    }

    private void dash()
    {
        transform.position += new Vector3(5, 0, 0);
    }

    private void jumpAttempt()
    {
        if (isTouchingGround == true || canDoubleJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
            canDoubleJump = false;
        }
    }

    private void handleCollisions()
    {
        isTouchingGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, ground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    }
}
