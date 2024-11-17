using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Animator animator;
    public float horizontal;
    public float vertical;
    public Rigidbody2D rb;
    public float speed = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("isMoving", false);
        animator.SetBool("facingUp", false);
        animator.SetBool("facingDown", true);
        animator.SetBool("facingLeft", false);
        animator.SetBool("facingRight", false);
    }

    // Update is called once per frame
    public void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (vertical != 0)
        {
            //animator.SetBool("isMoving", true);
            
            if (vertical < 0)
            {
                animator.SetBool("facingDown", true);
                animator.SetBool("facingUp", false);
            }
            else
            {
                animator.SetBool("facingDown", false);
                animator.SetBool("facingUp", true);
            }
        }
        else if (vertical == 0 && horizontal != 0)
        {
            //animator.SetBool("isMoving", true);
            
            animator.SetBool("facingUp", false);
            animator.SetBool("facingDown", false);

            if (horizontal < 0)
            {
                animator.SetBool("facingRight", false);
                animator.SetBool("facingLeft", true);
            }
            else
            {
                animator.SetBool("facingRight", true);
                animator.SetBool("facingLeft", false);
            }
        }

        if (rb.linearVelocity != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal, vertical).normalized * speed;
    }
}

