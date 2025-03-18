using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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
    [SerializeField] private float extraFallGravity = 0.5f; //The amount of exta gravity added to the player when falling and not holding the jump button

    [Header("Mask Modifers")]
    [SerializeField] private float speedCapMultiplacation = 2f;
    [SerializeField] private float accelerationMultiplaction = 2f;
    [SerializeField] private float jumpMultiplacation = 2f;
    [SerializeField] private float dashSpeed = 10f;

    public bool canDash = false;
    bool pressedDash = false;
    bool isGrounded = false;
    bool velCap = true;
    bool lastFrameGrounded = false;
    bool jumped = false;
    bool wallJumpBoxContactL = false;
    bool wallJumpBoxContactR = false;
    bool haningOnWall = false;
    bool wallHangL = false;
    bool wallHangR = false;
    bool holdingJump = false;
    Rigidbody2D rb;
    Transform trans;
    float defaultGravScale;
    float timer1JB = 10;
    float timer2JB = 10;
    float timer3WJ = 10;
    float timer4VC = 0;
    float timer5NGF = 0;
    float finalAccelc;
    float finalSpeedCap;

    //floats for actively applyed stats
    float trueAccel;
    float trueMaxSpeed;
    float trueJumpForce;

    bool ran1 = false;
    bool ran2 = false;
    bool ran3 = false;
    bool ran4 = false;

    bool maskState1S = false;
    bool maskState2J = false;
    bool maskState3D = false;

    SpriteRenderer SpriteSpeedMask;
    SpriteRenderer SpriteJumpMask;
    SpriteRenderer SpriteDashMask;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        defaultGravScale = rb.gravityScale;
    }
    private void Update()
    {
        //Handles jump buffer timer
        if (Input.GetKeyDown(KeyCode.Z))
        {
            timer1JB = 0;
        }
        //Mask state change code
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Jump Mask Active");
            maskState1S = false;
            maskState2J = true;
            maskState3D = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Speed Mask Active");
            maskState1S = true;
            maskState2J = false;
            maskState3D = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Dash Mask Active");
            maskState1S = false;
            maskState2J = false;
            maskState3D = true;
        }
        //Hanles player dash
        if (maskState3D && !isGrounded && Input.GetKeyDown(KeyCode.Z) && !haningOnWall)
        {
            pressedDash = true;
        }
    }
    void FixedUpdate()
    {
        //Handels multiplyers for masks
        if (maskState1S)
        {
            trueMaxSpeed = maxSpeed * speedCapMultiplacation;
            trueAccel = acceleration * accelerationMultiplaction;
        }
        else
        {
            trueAccel = acceleration;
            trueMaxSpeed = maxSpeed;
        }
        if (maskState2J)
        {
            trueJumpForce = jumpForce * jumpMultiplacation;
        }
        else
        {
            trueJumpForce = jumpForce;
        }
            //Checks if player is holding jump key
            holdingJump = Input.GetKey(KeyCode.Z);
        //timers
        timer1JB += Time.deltaTime;
        timer2JB += Time.deltaTime;
        timer3WJ += Time.deltaTime;
        timer4VC -= Time.deltaTime;
        //Check if player is in contact with ground
        isGrounded = Physics2D.OverlapBox(new Vector2(playerTransform.position.x, playerTransform.position.y - groundCheckOffsetY), new Vector2(groundCheckWidth, 0.1f), 0f, layerOfGround);
        //Only ticks timer if not grounded
        if (!isGrounded)
        {
            timer5NGF += Time.deltaTime;
        } else
        {
            canDash = true;
            timer5NGF = 0;
        }
        //Check if player is in contact with wall
        wallJumpBoxContactR = Physics2D.OverlapBox(new Vector2(playerTransform.position.x + wallGrabBoxOffsetX, playerTransform.position.y + wallGrabBoxPosY), new Vector2(0.1f, wallGrabBoxHightY), 0f, layerOfGround);
        wallJumpBoxContactL = Physics2D.OverlapBox(new Vector2(playerTransform.position.x + wallGrabBoxOffsetX * -1, playerTransform.position.y + wallGrabBoxPosY), new Vector2(0.1f, wallGrabBoxHightY), 0f, layerOfGround);
        //Resets player jump state when touching ground
        if (isGrounded && jumped)
        {
            Debug.Log("landed from jump");
            jumped = false;
        }
        //Handles toggling the velocity cap
        if (timer4VC < 0f)
        {
            velCap = true;
        } else
        {
            ran4 = false;
            velCap = false;
        }
        //Handels haning on wall
        if (Input.GetKey(KeyCode.LeftArrow) && wallJumpBoxContactL && !ran2)
        {
            canDash = true;
            ran1 = false;
            ran2 = true;
            ran3 = false;
            wallHangL = true;
            rb.gravityScale = 0;
            haningOnWall = true;
            rb.velocity = Vector2.zero;
            Debug.Log("wallClingL");
        }
        if (Input.GetKey(KeyCode.RightArrow) && wallJumpBoxContactR && !ran2)
        {
            canDash = true;
            ran1 = false;
            ran2 = true;
            ran3 = false;
            wallHangR = true;
            rb.gravityScale = 0;
            haningOnWall = true;
            rb.velocity = Vector2.zero;
            Debug.Log("wallClingR");
        }
        if (wallHangR && !Input.GetKey(KeyCode.RightArrow) && !ran1 || wallHangL && !Input.GetKey(KeyCode.LeftArrow) && !ran1 || ran3 && !ran1)
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
        if (Input.GetKey(KeyCode.LeftArrow) && !haningOnWall && timer3WJ >= 0.15)
        {
            rb.velocity = new Vector2(rb.velocity.x - trueAccel, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.RightArrow) && !haningOnWall && timer3WJ >= 0.15)
        {
            rb.velocity = new Vector2(rb.velocity.x + trueAccel, rb.velocity.y);
        }
        //Top speed cap
        if (velCap && rb.velocity.x > trueMaxSpeed)
        {
            if (rb.velocity.y > trueMaxSpeed && !ran4)
            {
                rb.velocity = new Vector2(rb.velocity.x, trueMaxSpeed);
                ran4 = true;
            }
            rb.velocity = new Vector2(trueMaxSpeed, rb.velocity.y);
            //rb.velocity = new Vector2 (rb.velocity.x * 0.92f, rb.velocity.y);
        }
        if (velCap && rb.velocity.x < trueMaxSpeed * -1)
        {
            if (rb.velocity.y > trueMaxSpeed && !ran4)
            {
                rb.velocity = new Vector2(rb.velocity.x, trueMaxSpeed);
                ran4 = true;
            }
            rb.velocity = new Vector2(trueMaxSpeed * -1, rb.velocity.y);
            //rb.velocity = new Vector2(rb.velocity.x * 0.92f, rb.velocity.y);
        }

        //Handles player jump condition
        if (timer1JB < 0.05 && (isGrounded || timer2JB < 0.1 && !jumped)) 
        {
            jumped = true;
            Jump();
            Debug.Log("Jumped");
        }
        //Handles player wall jump condition
        if (wallHangL && timer1JB < 0.05 && !ran3)
        {
            timer3WJ = 0;
            timer4VC = 0.3f;
            ran3 = true;
            rb.velocity = new Vector2(trueJumpForce * 1.2f, trueJumpForce);
        }
        if (wallHangR && timer1JB < 0.05 && !ran3)
        {
            timer4VC = 0.3f;
            timer3WJ = 0;
            ran3 = true;
            rb.velocity = new Vector2(trueJumpForce * -1.2f, trueJumpForce);
        }
        //Adds extra gravity to the player when falling and not holding jump button
        if (!holdingJump && !haningOnWall)
        {
            rb.gravityScale = defaultGravScale + extraFallGravity;
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
        //Handles dash condition
        if (pressedDash && canDash)
        {
            pressedDash = false;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Dash(true, false);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Dash(false, false);
            }
        }
    }
    void Jump()
    {
        rb.gravityScale = defaultGravScale;
        rb.velocity = new Vector2(rb.velocity.x, trueJumpForce);
    }
    void Dash(bool goingRight, bool digionalJump)
    {
        Debug.Log("Dashed");
        canDash = false;
        if (goingRight)
        {
            rb.velocity = new Vector2(dashSpeed * 5, rb.velocity.y);
            timer4VC = 0.05f;
        }
        if (!goingRight)
        {
            rb.velocity = new Vector2(dashSpeed * -5, rb.velocity.y);
            timer4VC = 0.05f;
        }
        if (digionalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, dashSpeed * 5);
        }
    }
    private void OnDrawGizmos()
    {
        //Draws ground check box
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(playerTransform.position.x, playerTransform.position.y - groundCheckOffsetY, playerTransform.position.z), new Vector3(groundCheckWidth, 0.1f, 0));
        //Draws wall jump box
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(playerTransform.position.x + wallGrabBoxOffsetX, playerTransform.position.y + wallGrabBoxPosY, playerTransform.position.z), new Vector3(0.1f, wallGrabBoxHightY, 0));
        Gizmos.DrawWireCube(new Vector3(playerTransform.position.x + wallGrabBoxOffsetX * -1, playerTransform.position.y + wallGrabBoxPosY, playerTransform.position.z), new Vector3(0.1f, wallGrabBoxHightY, 0));
    }
}
