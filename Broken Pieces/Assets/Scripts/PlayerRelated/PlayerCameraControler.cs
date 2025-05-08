using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControler : MonoBehaviour
{
    [SerializeField] private float respawnSpeed = 1;
    static GameObject player;
    static private bool cameraMovingTowardsPlayer = false;
    static private bool cameraCanMoveTowardsPlayer = false;
    static private float timer1 = 0;
    static private bool ran1 = false;
    static private TestMovementForPlayer playerMovementScript;
    static private PlayerAnimationControl playerACScript;
    private float cameraMoveSpeed = 0;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovementScript = player.GetComponent<TestMovementForPlayer>();
        playerACScript = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<PlayerAnimationControl>();


    }
    void FixedUpdate()
    {
        timer1 += Time.deltaTime;
        if (cameraCanMoveTowardsPlayer && !ran1 && timer1 > 0.70f)
        {
            cameraMoveSpeed = Vector3.Distance(transform.position, player.transform.position);
            Debug.Log(cameraMoveSpeed * respawnSpeed);
            ran1 = true;
        }
        if (cameraCanMoveTowardsPlayer && timer1 > 0.75f)
        {
            cameraMovingTowardsPlayer = true;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z), cameraMoveSpeed * respawnSpeed);
        }
        if (cameraCanMoveTowardsPlayer && transform.position.x == player.transform.position.x && transform.position.y == player.transform.position.y)
        {
            TestMovementForPlayer.playerCanMove = true;
            transform.SetParent(player.transform);
            cameraCanMoveTowardsPlayer = false;
            cameraMovingTowardsPlayer = false;
            Debug.Log("Camera Reparented");
        }
    }
    static public void PlayerCameraRespawnSequence()
    {
        GameObject.FindGameObjectWithTag("MainCamera").transform.parent = null;
        TestMovementForPlayer.playerCanMove = false;
        playerMovementScript.UnequiptMask();
        cameraCanMoveTowardsPlayer = true;
        timer1 = 0;
        ran1 = false;
    }
}
