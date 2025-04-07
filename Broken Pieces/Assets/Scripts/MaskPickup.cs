using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    [Header("Select The Desired mask")]
    [SerializeField] private bool giveSpeedMask = false;
    [SerializeField] private bool giveJumpMask = false;
    [SerializeField] private bool giveDashMask = false;
    [SerializeField] private SpriteRenderer speedMaskSPR; //the sprite renderers for the masks
    [SerializeField] private SpriteRenderer jumpMaskSPR;
    [SerializeField] private SpriteRenderer dashMaskSPR;
    [SerializeField] private GameObject noneSlected;
    private bool runTheCodeOfShame = false;
    private bool moreThanOneMaskSelected = false;
    private bool noMasksSelected =false;

    void Start()
    {
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
    void Update()
    {
        if (runTheCodeOfShame)
        {
            noneSlected.transform.Rotate(Vector3.up, Random.Range(0f, 1f), Space.Self);
            Debug.Log("UHH OHHH YOU FORGOR TO SET A MASK!! JUST BLAME IT ON SHADEN CUZ THIS SHIT AINT GONNA WORK");
        }
    }
}
