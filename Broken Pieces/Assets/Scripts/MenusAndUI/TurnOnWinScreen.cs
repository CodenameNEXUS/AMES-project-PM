using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnWinScreen : MonoBehaviour
{
    Canvas canvas;
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("WinScreen").GetComponent<Canvas>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           canvas.enabled = true;
            Time.timeScale = 0;
        }
    }
}
