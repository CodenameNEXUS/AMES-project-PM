using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointMannager : MonoBehaviour
{
    Vector3 respawnPoint;
    GameObject player;
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
    public void RespawnFromCheckpoint()
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
    public void SetCheckPoint(Transform checkpointTransform)
    {
        respawnPoint = checkpointTransform.position;
    }
}
