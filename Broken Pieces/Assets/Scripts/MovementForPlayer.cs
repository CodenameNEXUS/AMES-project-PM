using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementForPlayer : MonoBehaviour
{
    [Header("BasicConfig")]
    [SerializeField] private float groundCheckWidth = 0.3f; //Width of ground check box
    [SerializeField] private float groundCheckOffsetY = 0.5f; //Y offset of ground check box
    [SerializeField] private float wallGrabBoxPosY = 0; //Y Position of wall grab box
    [SerializeField] private float wallGrabBoxOffsetX = 0.55f; //X offset from the center of player for wall grab box
    [SerializeField] private float wallGrabBoxHightY = 0.5f; //Y hight of wall grab box
    [SerializeField] private LayerMask layerOfGround; //Layer that the ground is on
    [SerializeField] private Transform playerTransform; // Transform for ground check position

    [Header("Player Base Stats")]
    [SerializeField] private float maxSpeed = 6f; //Max base speed of player outside of dashing
    [SerializeField] private float acceleration = 2f; //Base player acceleration
    [SerializeField] private float jumpForce = 4f; //Base player jump force

    [Header("Mask Modifers")]
    [SerializeField] private float speedCapMultiplacation = 2f;
    [SerializeField] private float accelerationMultiplaction = 2f;
    [SerializeField] private float jumpMultiplacation = 2f;
    [SerializeField] private float dashSpeedBurst = 10f;

    bool isGrounded = false;
    bool velCap = true;
    bool lastFrameGrounded = false;
    bool jumped = false;
    bool wallJumpBoxContactL = false;
    bool wallJumpBoxContactR = false;
    bool haningOnWall = false;
    bool wallHangL = false;
    bool wallHangR = false;
    Rigidbody2D rb;
    Transform trans;
    float defaultGravScale;
    float timer1JB = 10;
    float timer2JB = 10;

    bool ran1 = false;
    bool ran2 = false;

    bool maskState1S = false;
    bool maskState2J = false;
    bool maskState3D = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        defaultGravScale = rb.gravityScale;
    }
    void FixedUpdate()
    {
        //timers
        timer1JB += Time.deltaTime;
        timer2JB += Time.deltaTime;
        //Check if player is in contact with ground
        isGrounded = Physics2D.OverlapBox(new Vector2(playerTransform.position.x, playerTransform.position.y - groundCheckOffsetY), new Vector2(groundCheckWidth, 0.1f), 0f, layerOfGround);
        //Check if player is in contact with wall
        wallJumpBoxContactR = Physics2D.OverlapBox(new Vector2(playerTransform.position.x + wallGrabBoxOffsetX, playerTransform.position.y + wallGrabBoxPosY), new Vector2(0.1f, wallGrabBoxHightY), 0f, layerOfGround);
        wallJumpBoxContactL = Physics2D.OverlapBox(new Vector2(playerTransform.position.x + wallGrabBoxOffsetX * -1, playerTransform.position.y + wallGrabBoxPosY), new Vector2(0.1f, wallGrabBoxHightY), 0f, layerOfGround);
        if (isGrounded && jumped)
        {
            Debug.Log("landed from jump");
            jumped = false;
        }
        //Handels haning on wall
        if (Input.GetKey(KeyCode.LeftArrow) && wallJumpBoxContactL && !ran2)
        {
            ran1 = false;
            ran2 = true;
            wallHangL = true;
            rb.gravityScale = 0;
            haningOnWall = true;
            rb.velocity = Vector2.zero;
            Debug.Log("wallClingL");
        }
        if (Input.GetKey(KeyCode.RightArrow) && wallJumpBoxContactR && !ran2)
        {
            ran1 = false;
            ran2 = true;
            wallHangR = true;
            rb.gravityScale = 0;
            haningOnWall = true;
            rb.velocity = Vector2.zero;
            Debug.Log("wallClingR");
        }
        if (wallHangR && !Input.GetKey(KeyCode.RightArrow) && !ran1 || wallHangL && !Input.GetKey(KeyCode.LeftArrow) && !ran1)
        {
            wallHangL = false;
            wallHangR = false;
            ran1 = true;
            ran2 = false;
            Debug.Log("let off wall");
            rb.gravityScale = defaultGravScale;
            haningOnWall = false;
        }
        //LR movement
        if (Input.GetKey(KeyCode.LeftArrow) && !haningOnWall)
        {
            rb.velocity = new Vector2(rb.velocity.x - acceleration, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.RightArrow) && !haningOnWall)
        {
            rb.velocity = new Vector2(rb.velocity.x + acceleration, rb.velocity.y);
        }
        //Ground speed cap
        if (velCap && rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        if (velCap && rb.velocity.x < maxSpeed * -1)
        {
            rb.velocity = new Vector2(maxSpeed * -1, rb.velocity.y);
        }

        //Handles player jump condition
        if (timer1JB < 0.2 && (isGrounded || timer2JB < 0.1 && !jumped)) 
        {
            jumped = true;
            Jump();
            Debug.Log("Jumped");
        }
        //Handles jump buffer timers
        if (Input.GetKey(KeyCode.Z))
        {
            timer1JB = 0;
        }
        //checks if player walks off terrain
        if (!isGrounded && lastFrameGrounded && !jumped)
        {
            timer2JB = 0;
            lastFrameGrounded = false;
            Debug.Log("walked of platform");
        }
        if (isGrounded)
        {
            lastFrameGrounded = true;
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
        //Draws ground check box
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(playerTransform.position.x, playerTransform.position.y - groundCheckOffsetY, playerTransform.position.z), new Vector3(groundCheckWidth, 0.1f, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(playerTransform.position.x + wallGrabBoxOffsetX, playerTransform.position.y + wallGrabBoxPosY, playerTransform.position.z), new Vector3(0.1f, wallGrabBoxHightY, 0));
        Gizmos.DrawWireCube(new Vector3(playerTransform.position.x + wallGrabBoxOffsetX * -1, playerTransform.position.y + wallGrabBoxPosY, playerTransform.position.z), new Vector3(0.1f, wallGrabBoxHightY, 0));
    }
}
