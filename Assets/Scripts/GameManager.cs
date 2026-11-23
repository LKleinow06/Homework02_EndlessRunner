using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI screens")]
    public GameObject startScreen;
    public GameObject gameplayUI;
    public GameObject gameOverScreen;
    public GameObject highScoresScreen;

    [Header("Score text")]
    public TMP_Text scoreText;
    public TMP_Text coinText;
    public TMP_Text finalScoreText;
    public TMP_Text highScoresText;

    [Header("Scoring")]
    public float distanceSpeed = 5f;
    public int pointsPerCoin = 10;

    private float distance;
    private int coinsCollected;
    private int finalScore;

    private bool isPlaying;
    private bool isGameOver;

    //used to distinguish "Play Again" from "Main Menu"
    private static bool startImmediately = false;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    void Start()
    {
        if (startImmediately)
        {
            startImmediately = false;
            BeginGame();
        }
        else
        {
            ShowStartScreen();
        }
    }

    void Update()
    {
        if (!isPlaying)
        {
            return;
        }

        //track the distance travelled by the player
        distance += distanceSpeed * Time.deltaTime;

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        int currentScore = CalculateScore();

        scoreText.text = "SCORE: " + currentScore;
        coinText.text = "COINS: " + coinsCollected;
    }

    int CalculateScore()
    {
        int distancePoints = Mathf.FloorToInt(distance);

        int coinPoints = coinsCollected * pointsPerCoin;

        return distancePoints + coinPoints;
    }

    public void CollectCoin()
    {
        if (!isPlaying || isGameOver)
        {
            return;
        }

        coinsCollected++;

        UpdateScoreUI();
    }

    public void GameOver()
    {
        if (isGameOver || !isPlaying)
        {
            return;
        }

        isGameOver = true;
        isPlaying = false;

        finalScore = CalculateScore();

        //save the player's score
        SaveHighScore(finalScore);

        //stop the game
        Time.timeScale = 0f;

        gameplayUI.SetActive(false);
        gameOverScreen.SetActive(true);

        finalScoreText.text = "FINAL SCORE: " + finalScore;
    }

    void BeginGame()
    {
        distance = 0f;
        coinsCollected = 0;

        isPlaying = true;
        isGameOver = false;

        Time.timeScale = 1f;

        startScreen.SetActive(false);
        gameplayUI.SetActive(true);
        gameOverScreen.SetActive(false);
        highScoresScreen.SetActive(false);

        UpdateScoreUI();
    }

    public void ShowStartScreen()
    {
        isPlaying = false;
        isGameOver = false;

        Time.timeScale = 0f;

        startScreen.SetActive(true);
        gameplayUI.SetActive(false);
        gameOverScreen.SetActive(false);
        highScoresScreen.SetActive(false);
    }

    public void PlayGame()
    {
        BeginGame();
    }

    public void PlayAgain()
    {
        startImmediately = true;

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        startImmediately = false;

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowHighScores()
    {
        startScreen.SetActive(false);
        gameplayUI.SetActive(false);
        gameOverScreen.SetActive(false);
        highScoresScreen.SetActive(true);

        UpdateHighScoresUI();
    }

    public void BackToMenu()
    {
        ShowStartScreen();
    }

    //save and maintain top 5 high scores
    void SaveHighScore(int score)
    {
        List<int> scores = LoadHighScores();

        scores.Add(score);

        scores = scores.OrderByDescending(s => s).Take(5).ToList();

        for (int i = 0; i < 5; i++)
        {
            int value = i < scores.Count ? scores[i] : 0;

            PlayerPrefs.SetInt("HighScore" + i, value);
        }

        PlayerPrefs.Save();
    }

    List<int> LoadHighScores()
    {
        List<int> scores = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            int score = PlayerPrefs.GetInt("HighScore" + i, 0);

            if (score > 0)
            {
                scores.Add(score);
            }
        }

        return scores.OrderByDescending(s => s).ToList();
    }

    void UpdateHighScoresUI()
    {
        List<int> scores = LoadHighScores();

        highScoresText.text = "HIGH SCORES\n\n";

        for (int i = 0; i < 5; i++)
        {
            highScoresText.text += (i + 1) + ". ";

            if (i < scores.Count)
            {
                highScoresText.text += scores[i];
            }
            else
            {
                highScoresText.text += "---";
            }

            highScoresText.text += "\n\n";
        }
    }
}