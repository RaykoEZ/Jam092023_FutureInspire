    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    public Animator animator;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            animator.SetFloat("Speed", 1.0f);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }

        if (horizontal != 0 && vertical != 0) 
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
