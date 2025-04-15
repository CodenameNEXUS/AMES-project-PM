using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    [Header("Select The Desired mask")]
    [SerializeField] private bool giveSpeedMask = false;
    [SerializeField] private bool giveJumpMask = false;
    [SerializeField] private bool giveDashMask = false;
    [SerializeField] private bool maskIdle = true;
    [SerializeField] private float maskIdleDistance = 0.1f;
    [SerializeField] private float maskIdleSpeed = 1.0f;
    [SerializeField] private GameObject speedMask; //the gameobjects of the masks
    [SerializeField] private GameObject jumpMask;
    [SerializeField] private GameObject dashMask;
    [SerializeField] private GameObject noneSlected;
    private bool runTheCodeOfShame = false;
    private float yPosOfObject;
    private SpriteRenderer speedMaskSPR; //the sprite renderers for the masks
    private SpriteRenderer jumpMaskSPR;
    private SpriteRenderer dashMaskSPR;
    private Transform speedMaskTRA;
    private Transform jumpMaskTRA;
    private Transform dashMaskTRA;
    private Transform noneSlectedTRA;
    private bool up = true;
    float currentOffset = 0;
    void Start()
    {
        speedMaskSPR = speedMask.GetComponent<SpriteRenderer>();
        jumpMaskSPR = jumpMask.GetComponent<SpriteRenderer>();
        dashMaskSPR = dashMask.GetComponent<SpriteRenderer>();
        speedMaskTRA = speedMask.transform;
        jumpMaskTRA = jumpMask.transform;
        dashMaskTRA = dashMask.transform;
        noneSlectedTRA = noneSlected.transform;
        yPosOfObject = transform.position.y;
        currentOffset = yPosOfObject;
        if (giveSpeedMask && !giveJumpMask && !giveDashMask)
        {
            speedMaskSPR.enabled = true;
        }
        if (giveJumpMask && !giveSpeedMask && !giveDashMask)
        {
            jumpMaskSPR.enabled = true;
        }
        if (giveDashMask && !giveJumpMask && !giveSpeedMask)
        {
            dashMaskSPR.enabled = true;
        }
        if (!giveDashMask && !giveJumpMask && !giveSpeedMask)
        {
            runTheCodeOfShame = true;
            noneSlected.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    void FixedUpdate()
    {
        if (maskIdle)
        {
            if (currentOffset >= yPosOfObject + maskIdleDistance)
            {
                up = false;
            } else if (currentOffset <= yPosOfObject - maskIdleDistance)
            {
                up = true;
            }
            if (up)
            {
                currentOffset = currentOffset + maskIdleSpeed * 0.01f;
            } else if (!up)
            {
                currentOffset = currentOffset - maskIdleSpeed * 0.01f;
            }
            speedMaskTRA.position = new Vector3(gameObject.transform.position.x, currentOffset, gameObject.transform.position.z);
            jumpMaskTRA.position = new Vector3(gameObject.transform.position.x, currentOffset, gameObject.transform.position.z);
            dashMaskTRA.position = new Vector3(gameObject.transform.position.x, currentOffset, gameObject.transform.position.z);
            noneSlectedTRA.position = new Vector3(gameObject.transform.position.x, currentOffset, gameObject.transform.position.z);
        }
        if (runTheCodeOfShame)
        {
            noneSlected.transform.Rotate(Vector3.up, 7, Space.Self);
            Debug.Log("UHH OHHH YOU FORGOR TO SET A MASK!! JUST BLAME IT ON SHADEN CUZ THIS SHIT AINT GONNA WORK");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<TestMovementForPlayer>() != null)
        {
            TestMovementForPlayer playerMovementScript = collision.gameObject.GetComponent<TestMovementForPlayer>();
            if (giveSpeedMask)
            { 
                playerMovementScript.playerCanUseSpeedMask = true;
                Debug.Log("Player Got Speed Mask");
            }
            if (giveJumpMask)
            {
                playerMovementScript.playerCanUseJumpMask = true;
                Debug.Log("Player Got Jump Mask");
            }
            if (giveDashMask)
            {
                playerMovementScript.playerCanUseDashMask = true;
                Debug.Log("Player Got Dash Mask");
            }
            if (!runTheCodeOfShame)
            {
                SelfDestruct(0);
            }
        }
    }
    private void SelfDestruct(float SelfDestructTime)
    {
        Destroy(gameObject, SelfDestructTime);
        Debug.Log("BOOM!!!");
    }
}
