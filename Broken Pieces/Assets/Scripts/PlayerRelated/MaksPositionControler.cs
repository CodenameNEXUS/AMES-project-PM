using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaksPositionControler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer speedMaskSPR;
    [SerializeField] private SpriteRenderer jumpMaskSPR;
    [SerializeField] private SpriteRenderer dashMaskSPR;
    private Transform maskMannager;
    private SpriteRenderer playerSPR;
    private Animator playerAC;
    string currentAnimation;
    AnimatorClipInfo[] animatorinfo;
    void Start()
    {
        maskMannager = gameObject.transform;
        playerAC = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<Animator>();
        playerSPR = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        animatorinfo = this.playerAC.GetCurrentAnimatorClipInfo(0);
        currentAnimation = animatorinfo[0].clip.name;
        if (currentAnimation == "PlayerIdle")
        {

        }
        if (currentAnimation == "PlayerRun")
        {

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
}
