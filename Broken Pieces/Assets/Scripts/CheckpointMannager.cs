using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointMannager : MonoBehaviour
{
    static Vector3 respawnPoint;
    static GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RespawnFromCheckpoint();
        }
    }
    static public void RespawnFromCheckpoint()
    {
        if (respawnPoint != Vector3.zero)
        {
            player.transform.position = respawnPoint;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Debug.Log("Respawning player at checkpoint");
        } else if (respawnPoint == Vector3.zero)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("No Checkpoint Found, Reloading Scene");
        }
    }
    static public void SetNewCheckPoint(Transform checkpointTransform)
    {
        respawnPoint = checkpointTransform.position;
    }
}
