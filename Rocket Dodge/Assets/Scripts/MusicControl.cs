using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour {

    private GameSound sound;
    public Button musicToggleButton;
    public Sprite musicOn;
    public Sprite musicOff;

    void Start()
    {

        sound = GameObject.FindObjectOfType<GameSound>();
        updateSound();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void pauseMusic()
    {
        sound.toggleSound();
        updateSound();
    }
    void updateSound()
    {
        if (PlayerPrefs.GetInt("muted", 0) == 0)
        {
            AudioListener.volume = 1;
            musicToggleButton.GetComponent<Image>().sprite = musicOn;
        }
        else
        {
            AudioListener.volume = 0;
            musicToggleButton.GetComponent<Image>().sprite = musicOff;
        }
    }
}
