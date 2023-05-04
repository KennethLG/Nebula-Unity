using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public float moveSpeed = 15f;
    public float jumpPower = 30;
    private float horizontal;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        UpdateSpriteFlip();
    }

    private void Movement()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        rb.drag = isGrounded ? 2f : 0.2f;
        Vector2 forceDirection = Quaternion.Euler(0, 0, -90) * transform.up;
        rb.AddForce(forceDirection * horizontal * moveSpeed);
    }

    private void UpdateSpriteFlip()
    {
        if (horizontal != 0)
        {
            spriteRenderer.flipX = Mathf.Sign(horizontal) == -1f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Planet"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Planet"))
        {
            isGrounded = false;
        }
    }
}
