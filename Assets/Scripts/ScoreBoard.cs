using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Scoreboard : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    private void Start()
    {
        score = 0;
        UpdateScoreboard();
    }

    private void UpdateScoreboard()
    {
        scoreText.text = "" + score;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreboard();
    }
}