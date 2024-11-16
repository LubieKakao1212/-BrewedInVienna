using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public Rigidbody2D rb;

    public float speed = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = new Vector2(horizontal * speed, vertical * speed);
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal, vertical).normalized * speed;
    }
}

