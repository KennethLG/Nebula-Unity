using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed, jumpPower;
    private float horizontal;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        }

        rb.drag = isGrounded ? 2f : 0.2f;
        Vector2 forceDirection = Quaternion.Euler(0, 0, -90) * transform.up;
        rb.AddForce(forceDirection * horizontal * moveSpeed);
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
