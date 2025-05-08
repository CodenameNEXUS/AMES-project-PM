using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControler : MonoBehaviour
{
    [SerializeField] private float respawnSpeed = 1;
    static GameObject player;
    static private bool cameraMovingTowardsPlayer = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (cameraMovingTowardsPlayer)
        {
            Vector3.MoveTowards(transform.position, player.transform.position, respawnSpeed);
            Debug.Log("player pos" + player.transform.position);
            Debug.Log("camera pos " + transform.position);
            //FIX THE Z AXIS WITH THE VECTOR3 MOVE
        }
    }
    static public void PlayerCameraRespawnSequence()
    {
        GameObject.FindGameObjectWithTag("MainCamera").transform.parent = null;
        cameraMovingTowardsPlayer = true;
    }
}
