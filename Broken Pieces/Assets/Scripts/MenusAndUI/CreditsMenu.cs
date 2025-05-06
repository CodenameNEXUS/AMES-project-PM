using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    Canvas canvas;
    Canvas winScreen;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        winScreen = GameObject.FindGameObjectWithTag("WinScreen").GetComponent<Canvas>();
    }
    public void Back()
    {
        canvas.enabled = false;
        winScreen.enabled = true;
    }
    public void Credits()
    {
        canvas.enabled = true;
        winScreen.enabled = false;
    }
}
