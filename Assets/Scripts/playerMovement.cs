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
        animator.SetInteger("verMovement", 0);
        animator.SetInteger("horMovement", 0);
    }

    // Update is called once per frame
    public void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        animator.SetInteger("horMovement", (int)(horizontal * 10));
        animator.SetInteger("verMovement", (int)(vertical * 10));
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal, vertical).normalized * speed;
    }
}

