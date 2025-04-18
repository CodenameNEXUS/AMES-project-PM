using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCode : MonoBehaviour
{
    [SerializeField] float cameraHorzBuffer;
    [SerializeField] float cameraVertBuffer;
    public bool cameraCanMove;
    GameObject player;
    Transform trans;
    Transform transPlayer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transPlayer = player.GetComponent<Transform>();
        trans = gameObject.GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        if (cameraCanMove)
        {
            CameraFollowPlayer();
        }
    }
    void CameraFollowPlayer()
    {
        if (cameraHorzBuffer > transPlayer.position.x - trans.position.x)
        {

        }
    }
}
