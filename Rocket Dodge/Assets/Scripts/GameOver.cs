using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameOver : MonoBehaviour
{

    public Text scoreText;

    public Text gameOver;



    public Image backgroundImg;
    public bool isShowned = false;
    private float transition = 0.1f;
    static int loadCount = 0;

    
    void Start()
    {

        // 3648298 - Google Play Ad ID
        // 3648299 - iOS Add ID
        Advertisement.Initialize("3648298");
        gameObject.SetActive(false);
        loadCount++;
    }

   
    void Update()
    {

        if (!isShowned)
            return;

        transition += Time.deltaTime;
        backgroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);


    }

    public void ToggleMenuScore(float score)
    {
        gameObject.SetActive(true);
        scoreText.text = "Your Score " + ((int)score).ToString();
        gameOver.text = "Game Over";

        isShowned = true;

        if (loadCount % 3 == 0)
        {

            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }

        }


    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Home()
    {


        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }


        SceneManager.LoadScene("Menu");
    }

}


