using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MaksPositionControler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer speedMaskSPR;
    [SerializeField] private SpriteRenderer jumpMaskSPR;
    [SerializeField] private SpriteRenderer dashMaskSPR;
    private SpriteRenderer playerSPR;
    private Animator playerAC;
    string currentAnimation;
    AnimatorClipInfo[] animatorinfo;
    int switchState = 0;
    void Start()
    {
        playerAC = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<Animator>();
        playerSPR = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        animatorinfo = this.playerAC.GetCurrentAnimatorClipInfo(0);
        currentAnimation = animatorinfo[0].clip.name;
        if (currentAnimation == "PlayerIdle")
        {
            Idle();
        }
        if (currentAnimation == "PlayerRun")
        {
            Run();
        }
        if (currentAnimation == "PlayerDash")
        {

        }
        if (currentAnimation == "PlayerWallHang")
        {

        }
        if(currentAnimation == "PlayerFalll")
        {

        }
        if (currentAnimation == "PlayerJump")
        {

        }
        if (playerSPR.flipX)
        {
            speedMaskSPR.flipX = true;
            jumpMaskSPR.flipX = true;
            dashMaskSPR.flipX = true;
        }
        else
        {
            speedMaskSPR.flipX = false;
            jumpMaskSPR.flipX = false;
            dashMaskSPR.flipX = false;
        }
    }
    private void Idle()
    {
        if (switchState == 0)
        {
            //transform.localPosition;
            switchState = 1;
        }
        else
        {
            switchState = 0;
        }
    }
    private void Run()
    {
        if (switchState == 0)
        {
            switchState = 1;
        }
        else
        {
            switchState = 0;
        }
    }
}
