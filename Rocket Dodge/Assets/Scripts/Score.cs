using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Score : MonoBehaviour
{
    private float score = 0.0f;
    private bool isDead = false;
    public GameOver deathMenu;



    public Text scoreText;

    void Start()
    {

        CloudOnceServices.instance.submitScoreToLeaderBoard((int)PlayerPrefs.GetFloat("highScore"));
    }

   
    void Update()
    {
        if (isDead)
            return;

        score += Time.deltaTime;
        scoreText.text = ((int)score).ToString();

    }

    public void onDeath()
    {
        isDead = true;

        CloudOnceServices.instance.submitScoreToLeaderBoard((int)score);

        if (PlayerPrefs.GetFloat("highScore") < score)
        {

            PlayerPrefs.SetFloat("highScore", score);
            PlayerPrefs.Save();
        }



        deathMenu.ToggleMenuScore(score);


    }
}
