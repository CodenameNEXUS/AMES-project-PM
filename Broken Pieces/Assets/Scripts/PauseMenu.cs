using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    Canvas canvas;
    float timeScaleDef;
    void Start()
    {
        timeScaleDef = Time.timeScale;
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canvas.enabled == false)
        {
            Pause();
        } else if (Input.GetKeyDown(KeyCode.Escape) && canvas.enabled == true)
        {
            Resume();
        }
    }
    public void Pause()
    {
        canvas.enabled = true;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        canvas.enabled = false;
        Time.timeScale = timeScaleDef;
    }
    public void MainMenu()
    {
        Time.timeScale = timeScaleDef;
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
