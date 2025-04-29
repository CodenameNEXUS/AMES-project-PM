using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    Animator playerAC;
    TestMovementForPlayer PlayerScript;
    Rigidbody2D playerRB;
    SpriteRenderer SPR;
    int playerVelY;
    int playerVelX;
    void Start()
    {
        SPR = GetComponent<SpriteRenderer>();
        playerAC = GetComponent<Animator>();
        PlayerScript = gameObject.GetComponentInParent<TestMovementForPlayer>();
        playerRB = gameObject.transform.parent.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        /*if (playerRB.velocity.x > -0.1 && playerRB.velocity.x < 0.1)
        {
            playerVelX = 0;
        }
        if (playerRB.velocity.x < -0.1)
        {
            playerVelX = -1;
        } else if (playerRB.velocity.x > 0.1)
        {
            playerVelX = 1;
        }*/
        if (playerRB.velocity.y > -0.1 && playerRB.velocity.y < 0.1)
        {
            playerVelY = 0;
        }
        if (playerRB.velocity.y < -0.1)
        {
            playerVelY = -1;
        } else if (playerRB.velocity.y > 0.1)
        {
            playerVelY = 1;
        }
        playerAC.SetBool("IsGrounded", PlayerScript.isGrounded);
        playerAC.SetBool("WallHangL", PlayerScript.wallHangL);
        playerAC.SetBool("WallHangR", PlayerScript.wallHangR);
        playerAC.SetInteger("VelocityX", playerVelX);
        playerAC.SetInteger("VelocityY", playerVelY);
    }
}