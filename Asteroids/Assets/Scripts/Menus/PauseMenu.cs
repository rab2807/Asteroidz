using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        AudioManager.Play(AudioName.Button);

        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public void Exit()
    {
        AudioManager.Play(AudioName.Button);

        Resume();
        MenuManager.GoTo(MenuName.Main);
    }
}