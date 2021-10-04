using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private InputField inputField;
    [SerializeField] private Button replayButton, exitButton;

    private int score;

    private void Start()
    {
        Time.timeScale = 0;
        score = GameObject.FindGameObjectWithTag("HUD").GetComponent<GamePlayMenu>().Score;
        scoreText.text = "Your score: " + score;
        replayButton.interactable = exitButton.interactable = false;
        replayButton.GetComponentInChildren<Text>().color =
            exitButton.GetComponentInChildren<Text>().color = Color.gray;
        GameObject.FindGameObjectWithTag("HUD").GetComponentInChildren<Button>().interactable = false;
    }

    public void Submit()
    {
        AudioManager.Play(AudioName.Button);

        string name = inputField.text.Trim();
        if (!name.Equals(""))
        {
            replayButton.interactable = exitButton.interactable = true;
            replayButton.GetComponentInChildren<Text>().color =
                exitButton.GetComponentInChildren<Text>().color = Color.white;
            inputField.interactable = false;
            // process high score
            HighScore.UpdateData(name, score);
        }
    }

    public void Replay()
    {
        AudioManager.Play(AudioName.Button);

        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoTo(MenuName.Gameplay);
    }

    public void Exit()
    {
        AudioManager.Play(AudioName.Button);

        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoTo(MenuName.Main);
    }
}