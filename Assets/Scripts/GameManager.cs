using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PCController;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GameStart,
        Wait,
        GameRestart,
        LevelVictory,

    }

    public PCController PCController;
    public GameState currentGamestate;
    public TextMeshProUGUI countdownText;

    public ScoreManager scoreManager; // Reference to the ScoreManager




    public static GameManager instance;

    private float countdownTimer = 5f;

    

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentGamestate = GameState.Wait;
        PCController = FindAnyObjectByType<PCController>();
        StartCountdown();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentGamestate)
        {
            case GameState.GameStart:
                break;
            case GameState.Wait:
                UpdateCountdownTimer();
                break;
            case GameState.GameRestart:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;

        }
    }

    void UpdateCountdownTimer()
    {
        countdownTimer -= Time.deltaTime;

        // Update UI text
        countdownText.text = Mathf.Ceil(countdownTimer).ToString();
        if (countdownTimer <= 0)
        {
           
            currentGamestate = GameState.GameStart;

            countdownText.text = "";
        }

    }

    public void StartCountdown()
    {
        currentGamestate = GameState.Wait;
    }

    public void ChangeState(GameState state)
    {
        currentGamestate = state;
    }

    




}
