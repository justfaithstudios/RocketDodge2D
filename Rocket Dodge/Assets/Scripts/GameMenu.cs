using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameMenu : MonoBehaviour
{
    public Text highScoreText;
    private const string TWITTER_ADDRESS = "http://twitter.com/imjaewilliams";
    private const string FACEBOOK_ADDRESS = "http://facebook.com/realjaewilliams";
    private const string Instagram_ADDRESS = "http://instagram.com/imjaewilliams";
    private const string allLinks = "https://solo.to/imjaewilliams";

    public GameObject creditsScreen;

    void Start()
    {


        highScoreText.text = "Best Score " + (int)PlayerPrefs.GetFloat("highScore");


    }

    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Instagram()
    {
        Application.OpenURL(Instagram_ADDRESS);
    }



    public void Twitter()
    {
        Application.OpenURL(TWITTER_ADDRESS);
    }


    public void MyLinks()
    {
        Application.OpenURL(allLinks);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToCreditsScreen()
    {
        creditsScreen.SetActive(true);
    }


    public void ExitreditsScreen()
    {
        creditsScreen.SetActive(false);
    }


}
