using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayMenu : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int score;

    private void Start()
    {
        scoreText.text = score.ToString();
    }

    public int Score => score;

    public void UpdateScore(int t)
    {
        score += t;
        scoreText.text = score.ToString();
    }
}