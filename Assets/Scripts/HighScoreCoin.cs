using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreCoin : MonoBehaviour
{
  
    public int scoreValue = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Scoreboard scoreManager = FindObjectOfType<Scoreboard>();
            if (scoreManager != null)
            {
                scoreManager.IncreaseScore(scoreValue);
            }

            // Additional pickup logic goes here.
            Destroy(gameObject); // Destroy the pickup.
        }
    }
}
