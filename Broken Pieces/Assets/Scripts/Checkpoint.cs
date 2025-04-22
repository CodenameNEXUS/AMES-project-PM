using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private float IdleDistance = 0.1f;
    [SerializeField] private float IdleSpeed = 1.0f;
    [SerializeField] private GameObject checkpointClaimed;
    [SerializeField] private GameObject checkpointNotClaimed;
    private float yPosOfObject;
    private Transform notClaimedTRA;
    private Transform claimedTRA;
    private SpriteRenderer notClaimedSPR;
    private SpriteRenderer claimedSPR;
    private bool up = true;
    private bool claimed = false;
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
        if (currentOffset >= yPosOfObject + IdleDistance)
        {
            up = false;
        }
        else if (currentOffset <= yPosOfObject - IdleDistance)
        {
            up = true;
        }
        if (up)
        {
            currentOffset = currentOffset + IdleSpeed * 0.01f;
        }
        else if (!up)
        {
            currentOffset = currentOffset - IdleSpeed * 0.01f;
        }
        notClaimedTRA.position = new Vector3(gameObject.transform.position.x, currentOffset, gameObject.transform.position.z);
        claimedTRA.position = new Vector3(gameObject.transform.position.x, currentOffset, gameObject.transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !claimed)
        {
            claimed = true;
            Debug.Log("New Checkpoint Claimed");
            notClaimedSPR.enabled = false;
            claimedSPR.enabled = true;
            CheckpointMannager.SetNewCheckPoint(gameObject.transform);
        }
    }

}
