using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private int levelToLoad;
    [SerializeField] private float fadeToBlackTime = 1;
    private float timer = -2;
    private Image blackBox;
    private bool fadingToBlack = false;
    private bool ran1 = false;
    public void Start()
    {
        blackBox = gameObject.GetComponentInChildren<Image>();
        blackBox.color = new Color(blackBox.color.r, blackBox.color.g, blackBox.color.b, 1f);
    }
    public void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            blackBox.color =  new Color(blackBox.color.r, blackBox.color.g, blackBox.color.b, blackBox.color.a + (0.03f/fadeToBlackTime));
            if (!ran1)
            {
                ran1 = true;
                blackBox.color = new Color(blackBox.color.r, blackBox.color.g, blackBox.color.b, 0f);
            }
        } else if ( timer < 0 && timer > -1)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        if (!fadingToBlack)
        {
            blackBox.color = new Color(blackBox.color.r, blackBox.color.g, blackBox.color.b, blackBox.color.a - (0.03f / fadeToBlackTime));
        }
    }
    private void LoadLevel()
    {
        fadingToBlack = true;
        timer = fadeToBlackTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LoadLevel();
        }
    }
}
