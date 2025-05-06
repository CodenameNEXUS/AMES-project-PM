using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIAnimationSmallButton : MonoBehaviour
{
    Image image;
    UIImages uiImages;
    Canvas canvas;
    int random;
    int timer;
    int timerThresh;
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiImages = GameObject.FindGameObjectWithTag("UIImageObject").GetComponent<UIImages>();
        image = GetComponent<Image>();
        timerThresh = 100;
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            timerThresh = timerThresh * 2;
        }

    }
    void Update()
    {
        timer += 1;
        if (timer > timerThresh && canvas.enabled)
        {
            int oldRan = random;
            
            if (random == oldRan)
            {
                GenerateRandomInt();
            }
            if (random == oldRan)
            {
                GenerateRandomInt();
            }
            if (random == oldRan)
            {
                GenerateRandomInt();
            }
            if (random == oldRan)
            {
                GenerateRandomInt();
            }
            ChangeImage();
            timer = 0;
        }
    }
    private void ChangeImage()
    {
        if (random == 0)
        {
            image.sprite = uiImages.UIImage5;
        }
        else if (random == 1)
        {
            image.sprite = uiImages.UIImage6;
        }
        else if (random == 2)
        {
            image.sprite = uiImages.UIImage7;
        }
        else if (random == 3)
        {
            image.sprite = uiImages.UIImage8;
        }
    }
    private void GenerateRandomInt()
    {
        random = Random.Range(0, 4);
    }
}
