using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Animator PCAnimation;
    public enum GameStates
    {
        Wait,
        GameStart,
        Restart,
        LevelVictory
    }

    public GameStates GameState;
    public static GameManager instance;

    public TextMeshProUGUI CountDownText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        GameState = GameStates.Wait;
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {
        switch (GameState)
        {
            case GameStates.Wait:
                break;

            case GameStates.GameStart:
                break;

            case GameStates.Restart:
                //RestartGame();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case GameStates.LevelVictory:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
                break;
        }
    }

    IEnumerator StartCountdown()
    {
        int countdownTime = 3;

        while (countdownTime > 0)
        {
            CountDownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }

        CountDownText.text = "GO!";
        yield return new WaitForSeconds(1);
        PCAnimation.SetBool("IsGameStart", true);
        CountDownText.gameObject.SetActive(false);
        GameState = GameStates.GameStart;
    }

    public void ResetGame()
    {
        GameManager.instance.ChangeState(GameManager.GameStates.GameStart);
    }

    public void RestartGame()
    {
        // You can add any additional logic here for resetting the game state
        // For now, let's just reset the game to the initial state
      
        GameManager.instance.ChangeState(GameManager.GameStates.Restart);
    }

    public void ChangeState(GameStates state)
    {
        GameState = state;
    }
}