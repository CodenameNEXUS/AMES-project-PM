using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    public float moveSpeed = 5f;
    bool isfaceingRight = false;
    public float jumpPower = 4f;
    public bool isJumping = true;


    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        flipSprite();
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontalInput * moveSpeed, rb.velocity.y);
    }
    void flipSprite()
    {
        if(isfaceingRight && horizontalInput < 0f || !isfaceingRight && horizontalInput > 0f)
        {
            isfaceingRight = !isfaceingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isJumping = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        isJumping = false;
    }
}
