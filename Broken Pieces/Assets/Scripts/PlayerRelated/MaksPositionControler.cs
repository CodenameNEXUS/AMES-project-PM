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
    private Transform playerSpriteTRANS;
    string currentSpriteFrame;
    //AnimatorClipInfo[] animatorinfo;
    Vector3 defaultPosOfMaskMannager;
    Vector3 defaultPosOfPlayerSprite;
    int switchState = 0;
    void Start()
    {
        playerAC = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<Animator>();
        playerSPR = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>();
        playerSpriteTRANS = GameObject.FindGameObjectWithTag("PlayerSprite").transform;
        defaultPosOfMaskMannager = transform.localPosition;
        defaultPosOfPlayerSprite = playerSpriteTRANS.localPosition;
    }
    void Update()
    {
        // temp vector storage for setup
        Vector3 eee = new Vector3 (0.16f, 0.22f, 0);
        Vector3 Default = new Vector3 (0.12f, 0.22f, 0);
        //animatorinfo = this.playerAC.GetCurrentAnimatorClipInfo(0);
        //currentAnimation = animatorinfo[0].clip.name;
        // One pixel worth of offset is about 0.04
        currentSpriteFrame = playerSPR.sprite.name;
        if (currentSpriteFrame == "Player_0")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Player_1")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y - 0.04f, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Player_2")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Player_3")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y - 0.04f, defaultPosOfMaskMannager.z);
        }
        if(currentSpriteFrame == "Player_4")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Player_5")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Player_6")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y + 0.04f, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Player_7")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y + 0.08f, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Player_8")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y + 0.04f, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Player_14")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x + 0.12f, defaultPosOfMaskMannager.y + 0.11f, defaultPosOfMaskMannager.z);
            playerSpriteTRANS.localPosition = new Vector3(defaultPosOfPlayerSprite.x + 0.31f, defaultPosOfPlayerSprite.y - 0.2f, defaultPosOfPlayerSprite.z);
        }
        if (currentSpriteFrame == "Player_16")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y + 0.08f, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Player_17")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y + 0.12f, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Player_18")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y + 0.16f, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Player_19")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y + 0.24f, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Player_20")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y + 0.20f, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Player_21")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y + 0.16f, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame == "Characterguy_30")
        {
            transform.localPosition = new Vector3(defaultPosOfMaskMannager.x, defaultPosOfMaskMannager.y + 0.04f, defaultPosOfMaskMannager.z);
        }
        if (currentSpriteFrame != "Player_14")
        {
            playerSpriteTRANS.localPosition = defaultPosOfPlayerSprite;
        }
        if (playerSPR.flipX)
        {
            transform.localPosition = new Vector3(transform.localPosition.x * -1, transform.localPosition.y, transform.localPosition.z);
            speedMaskSPR.flipX = true;
            jumpMaskSPR.flipX = true;
            dashMaskSPR.flipX = true;
            if (currentSpriteFrame == "Player_14")
            {
                playerSpriteTRANS.localPosition = new Vector3(defaultPosOfPlayerSprite.x - 0.31f, defaultPosOfPlayerSprite.y - 0.2f, defaultPosOfPlayerSprite.z);
            }
        }
        else
        {
            speedMaskSPR.flipX = false;
            jumpMaskSPR.flipX = false;
            dashMaskSPR.flipX = false;
        }
    }
}
