using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMenu : MonoBehaviour
{
    [SerializeField] private Text scoreTable;
    private List<Tuple<string, int>> scoreChart;

    private void Start()
    {
        scoreChart = HighScore.ScoreChart;
        SetText();
    }

    private void SetText()
    {
        StringBuilder sb = new StringBuilder();
        int i = 1;
        foreach (var score in scoreChart)
        {
            sb.Append(i++ + "." + "  ");
            sb.Append(score.Item1 + "  -  ");
            sb.Append(score.Item2);
            sb.Append("\n");
        }

        scoreTable.text = sb.ToString();
    }

    public void Back()
    {
        AudioManager.Play(AudioName.Button);

        MenuManager.GoTo(MenuName.Main);
    }
}