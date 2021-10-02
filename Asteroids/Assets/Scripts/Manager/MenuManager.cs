using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static void GoTo(MenuName name)
    {
        if (name.Equals(MenuName.Main))
            SceneManager.LoadScene("MainMenu");
        else if (name.Equals(MenuName.Gameplay))
            SceneManager.LoadScene("GamePlay");
        else if (name.Equals(MenuName.Help))
            SceneManager.LoadScene("HelpMenu");
        else if (name.Equals(MenuName.HighScore))
            SceneManager.LoadScene("HighScoreMenu");
        else if (name.Equals(MenuName.Pause))
            Object.Instantiate(Resources.Load("PausePrefab"));
        else if (name.Equals(MenuName.GameOver))
            Instantiate(Resources.Load("GameOverPrefab"));
    }
}