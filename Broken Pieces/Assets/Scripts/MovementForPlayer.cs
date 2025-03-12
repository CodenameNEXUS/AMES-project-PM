using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementForPlayer : MonoBehaviour
{
    [Header("BasicConfig")]
    [SerializeField] private float groundCheckWidth = 0.3f; //Radi of ground check circle
    [SerializeField] private float groundCheckOffsetY = 0.5f; //Y offset of ground check circle
    [SerializeField] private LayerMask layerOfGround; //Layer that the ground is on
    [SerializeField] private Transform playerTransform; // Transform for ground check position

    [Header("Player Base Stats")]
    [SerializeField] private float maxGroundSpeed = 6f;
    [SerializeField] private float acceleration = 2f; //Base player speed
    [SerializeField] private float jumpForce = 4f; //Base player jump force

    [Header("Mask Modifers")]
    [SerializeField] private float speedMultiplacation = 2f;

    bool isGrounded = false;
    Rigidbody2D rb;
    Transform trans;
    float timer1JB = 0;
    float timer2JB = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x - acceleration, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x + acceleration, rb.velocity.y);
        }

        // Check if player is in contact with ground
        isGrounded = Physics2D.OverlapBox(new Vector2(playerTransform.position.x, playerTransform.position.y - groundCheckOffsetY), new Vector2(groundCheckWidth, 0.1f), 0f, layerOfGround);
        //handles player jump condition
        if (Input.GetKey(KeyCode.Z) && isGrounded) 
        {
            Jump();
            Debug.Log("Jumped");
        }
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    void Dash()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(playerTransform.position.x, playerTransform.position.y - groundCheckOffsetY, playerTransform.position.z), new Vector3(groundCheckWidth, 0.1f, 0));
    }
}
