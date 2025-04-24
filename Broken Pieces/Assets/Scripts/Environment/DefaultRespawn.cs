using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultRespawn : MonoBehaviour
{
    void Start()
    {
        CheckpointMannager.SetNewCheckPoint(gameObject.transform);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(1,2,0));
    }
}
