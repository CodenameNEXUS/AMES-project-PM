using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [Header("Select The Desired mask")]
    [SerializeField] private bool maskIdle = true;
    [SerializeField] private float maskIdleDistance = 0.1f;
    [SerializeField] private float maskIdleSpeed = 1.0f;
    [SerializeField] private GameObject checkpointClaimed;
    [SerializeField] private GameObject checkpointNotClaimed;
    private float yPosOfObject;
    private Transform notClaimedTRA;
    private Transform claimedTRA;
    private SpriteRenderer notClaimedSPR;
    private SpriteRenderer claimedSPR;
    private bool up = true;
    float currentOffset = 0;
    void Start()
    {
        claimedTRA = checkpointClaimed.transform;
        notClaimedTRA = checkpointNotClaimed.transform;
        yPosOfObject = transform.position.y;
        currentOffset = yPosOfObject;
        notClaimedSPR = checkpointNotClaimed.GetComponent<SpriteRenderer>();
        claimedSPR = checkpointClaimed.GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (currentOffset >= yPosOfObject + maskIdleDistance)
        {
            up = false;
        }
        else if (currentOffset <= yPosOfObject - maskIdleDistance)
        {
            up = true;
        }
        if (up)
        {
            currentOffset = currentOffset + maskIdleSpeed * 0.01f;
        }
        else if (!up)
        {
            currentOffset = currentOffset - maskIdleSpeed * 0.01f;
        }
        notClaimedTRA.position = new Vector3(gameObject.transform.position.x, currentOffset, gameObject.transform.position.z);
        claimedTRA.position = new Vector3(gameObject.transform.position.x, currentOffset, gameObject.transform.position.z);
    }

}
