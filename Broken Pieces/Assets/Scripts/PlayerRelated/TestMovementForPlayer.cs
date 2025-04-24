using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class TestMovementForPlayer : MonoBehaviour
{
    [Header("BasicConfig")]
    [SerializeField] private float groundCheckWidth = 0.75f; //Width of ground check box
    [SerializeField] private float groundCheckOffsetY = 0.1f; //Y offset of ground check box
    [SerializeField] private float wallGrabBoxPosY = 0; //Y Position of wall grab box
    [SerializeField] private float wallGrabBoxOffsetX = 0.55f; //X offset from the center of player for wall grab box
    [SerializeField] private float wallGrabBoxHightY = 0.5f; //Y hight of wall grab box
    [SerializeField] private float diagonalBoxOffsetX = 0.13f; //X offset of the diagonal boxes
    [SerializeField] private float diagonalBoxOffsetY = 0.15f; //Y offset of the diagonal boxes
    [SerializeField] private LayerMask layerOfGround; //Layer that the ground is on
    [SerializeField] private Transform playerTransform; // Transform for ground check position
    [SerializeField] private bool playerCanDiagonalDash = true; //Toggles weather player can do a diagonal dash
    public bool playerCanUseSpeedMask = true; //bools for toggleing weather the player can use a specif mask
    public bool playerCanUseJumpMask = true;
    public bool playerCanUseDashMask = true;
    public bool playerCanWallHang = true;
    [SerializeField] private SpriteRenderer speedMask; //the sprite renderers for the masks
    [SerializeField] private SpriteRenderer jumpMask;
    [SerializeField] private SpriteRenderer dashMask;

    [Header("Player Base Stats")]
    [SerializeField] private float maxSpeed = 6f; //Max base speed of player outside of dashing
    [SerializeField] private float acceleration = 0.5f; //Base player acceleration
    [SerializeField] private float jumpForce = 8f; //Base player jump force
    [SerializeField] private float extraFallGravity = 0.5f; //The amount of exta gravity added to the player when falling and not holding the jump button
    [SerializeField] private float groundSpeedFalloff = 0.8f;

    [Header("Mask Modifers")]
    [SerializeField] private float speedCapMultiplacation = 2f;
    [SerializeField] private float accelerationMultiplaction = 2f;
    [SerializeField] private float jumpMultiplacation = 1.5f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.06f;
    [Header("DONT CHANGE IN EDITOR THIS STUFF IS FOR ANIMATION CONTROL")]
    public bool canDash = false;
    public bool isGrounded = false;
    bool velCap = true;
    bool lastFrameGrounded = false;
    bool jumped = false;
    bool wallJumpBoxContactL = false;
    bool wallJumpBoxContactR = false;
    bool haningOnWall = false;
    public bool wallHangL = false;
    public bool wallHangR = false;
    bool holdingJump = false;
    bool lastFrameSpeedMask;
    bool runningCode = false;
    Rigidbody2D rb;
    Transform trans;
    float defaultGravScale;
    public float timer1JB = 10;
    float timer2JB = 10;
    float timer3WJ = 10;
    float timer4VC = 0;
    float timer6DB = 10;
    float timer7SMSB = 10;
    float finalAccelc;
    float finalSpeedCap;
    
    bool onDaigL = false;
    bool onDaigR = false;

    //floats for actively applyed stats
    float trueAccel;
    float trueMaxSpeed;
    float trueJumpForce;
    //ran bools
    bool ran1 = false;
    bool ran2 = false;
    bool ran3 = false;
    bool ran4 = false;
    //bools for different mask states
    bool maskState1S = false;
    bool maskState2J = false;
    bool maskState3D = false;
    void Start()
    {
        speedMask.enabled = false;
        jumpMask.enabled = false;
        dashMask.enabled = false;
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
        if (Input.GetKeyDown(KeyCode.A) && playerCanUseJumpMask)
        {
            Debug.Log("Jump Mask Active");
            speedMask.enabled = false;
            jumpMask.enabled = true;
            dashMask.enabled = false;
            maskState1S = false;
            maskState2J = true;
            maskState3D = false;
        }
        if (Input.GetKeyDown(KeyCode.S) && playerCanUseSpeedMask)
        {
            Debug.Log("Speed Mask Active");
            speedMask.enabled = true;
            jumpMask.enabled = false;
            dashMask.enabled = false;
            maskState1S = true;
            maskState2J = false;
            maskState3D = false;
        }
        if (Input.GetKeyDown(KeyCode.D) && playerCanUseDashMask)
        {
            Debug.Log("Dash Mask Active");
            speedMask.enabled = false;
            jumpMask.enabled = false;
            dashMask.enabled = true;
            maskState1S = false;
            maskState2J = false;
            maskState3D = true;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Unequipted Mask");
            speedMask.enabled = false;
            jumpMask.enabled = false;
            dashMask.enabled = false;
            maskState1S = false;
            maskState2J = false;
            maskState3D = false;
        }
        //Hanles player dash
        if (maskState3D && !isGrounded && Input.GetKeyDown(KeyCode.Z) && !haningOnWall)
        {
            timer6DB = 0;
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
        timer6DB += Time.deltaTime;
        timer7SMSB += Time.deltaTime;
        //Check if player is in contact with ground
        isGrounded = Physics2D.OverlapBox(new Vector2(playerTransform.position.x, playerTransform.position.y - groundCheckOffsetY), new Vector2(groundCheckWidth, 0.1f), 0f, layerOfGround);
        //Handes going up ramps
        if (Physics2D.OverlapBox(new Vector2(playerTransform.position.x + diagonalBoxOffsetX + wallGrabBoxOffsetX * -1, playerTransform.position.y - groundCheckOffsetY + diagonalBoxOffsetY), new Vector2(0.1f, 0.1f), 0f, layerOfGround))
        {
            onDaigL = true;
        }
        if (Physics2D.OverlapBox(new Vector2(playerTransform.position.x - diagonalBoxOffsetX + wallGrabBoxOffsetX, playerTransform.position.y - groundCheckOffsetY + diagonalBoxOffsetY), new Vector3(0.1f, 0.1f), 0f, layerOfGround))
        {
            onDaigR = true;
        }
        if (!Physics2D.OverlapBox(new Vector2(playerTransform.position.x + diagonalBoxOffsetX + wallGrabBoxOffsetX * -1, playerTransform.position.y - groundCheckOffsetY + diagonalBoxOffsetY), new Vector2(0.1f, 0.1f), 0f, layerOfGround))
        {
            onDaigL = false;
        }
        if (!Physics2D.OverlapBox(new Vector2(playerTransform.position.x - diagonalBoxOffsetX + wallGrabBoxOffsetX, playerTransform.position.y - groundCheckOffsetY + diagonalBoxOffsetY), new Vector3(0.1f, 0.1f), 0f, layerOfGround))
        {
            onDaigR = false;
        }
        if (isGrounded && onDaigL && !haningOnWall && Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 0.3f);
        }
        if (isGrounded && onDaigR && !haningOnWall && Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 0.3f);
        }
        if (isGrounded && (onDaigR || onDaigL) && !haningOnWall && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.3f);
        }
        if (isGrounded)
        {
            canDash = true;
        }
        //Resets timer when switching of speed mask (for vel cap)
        if (lastFrameSpeedMask && !maskState1S)
        {
            timer7SMSB = 0;
        }
        if (maskState1S)
        {
            lastFrameSpeedMask = true;
        }
        else
        {
            lastFrameSpeedMask = false;
        }
        //Sets max speed when switching off speed mask
        if (timer7SMSB < 0.5 && isGrounded)
        {
            trueMaxSpeed = maxSpeed * speedCapMultiplacation;
        }
        //Keeps top speed at speed mask cap wile in air and switched off speed mask
        if (timer1JB < 0.05 && !maskState1S && trueMaxSpeed == speedCapMultiplacation * maxSpeed)
        {
            runningCode = true;
        }
        if (runningCode && isGrounded && timer1JB >= 0.05)
        {
            runningCode = false;
        }
        if (runningCode)
        {
            trueMaxSpeed = maxSpeed * speedCapMultiplacation;
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
        if (Input.GetKey(KeyCode.LeftArrow) && wallJumpBoxContactL && !ran2 && playerCanWallHang && !isGrounded && timer1JB > 0.5f)
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
        if (Input.GetKey(KeyCode.RightArrow) && wallJumpBoxContactR && !ran2 && playerCanWallHang && !isGrounded && timer1JB > 0.5f)
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
        if (wallHangR && !Input.GetKey(KeyCode.RightArrow) && !ran1 || wallHangL && !Input.GetKey(KeyCode.LeftArrow) && !ran1 || ran3 && !ran1 || !ran1 && !wallJumpBoxContactL && !wallJumpBoxContactR)
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
        if (timer6DB < 0.1 && canDash)
        {
            if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow)|| Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow))
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    Dash(true, false);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Dash(false, false);
                }

            } else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) && playerCanDiagonalDash|| Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow) && playerCanDiagonalDash)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    Dash(true, true);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Dash(false, true);
                }
            }
        }
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && isGrounded)
        {
            rb.velocity = new Vector2 (rb.velocity.x * groundSpeedFalloff, rb.velocity.y);
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
            rb.velocity = new Vector2(dashSpeed * 5, 0.5f);
            timer4VC = dashDuration;
        }
        if (!goingRight)
        {
            rb.velocity = new Vector2(dashSpeed * -5, 0.5f);
            timer4VC = dashDuration;
        }
        if (digionalJump)
        {
            rb.velocity = new Vector2((rb.velocity.x/5) * 2f, dashSpeed * 2f);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3 && timer4VC > -0.1 && !canDash)
        {
            Debug.Log("hit wall wile dashing");
            rb.velocity = Vector2.zero;
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
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(new Vector3(playerTransform.position.x + diagonalBoxOffsetX + wallGrabBoxOffsetX * -1 , playerTransform.position.y - groundCheckOffsetY + diagonalBoxOffsetY, playerTransform.position.z), new Vector3(0.1f, 0.1f, 0));
        Gizmos.DrawWireCube(new Vector3(playerTransform.position.x - diagonalBoxOffsetX + wallGrabBoxOffsetX, playerTransform.position.y - groundCheckOffsetY + diagonalBoxOffsetY, playerTransform.position.z), new Vector3(0.1f, 0.1f, 0));
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y - groundCheckOffsetY, transform.position.z), -Vector3.up);
    }
}
