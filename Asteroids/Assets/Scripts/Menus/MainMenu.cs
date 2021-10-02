using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if (!HighScore.DataLoaded)
            HighScore.ReadData();
    }

    public void Play()
    {
        AudioManager.Play(AudioName.Button);

        MenuManager.GoTo(MenuName.Gameplay);
    }

    public void HighScoreButton()
    {
        AudioManager.Play(AudioName.Button);

        MenuManager.GoTo(MenuName.HighScore);
    }

    public void Help()
    {
        AudioManager.Play(AudioName.Button);

        MenuManager.GoTo(MenuName.Help);
    }

    public void Exit()
    {
        AudioManager.Play(AudioName.Button);

        Application.Quit();
    }
}