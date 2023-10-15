using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public BirdController birdController;
    public PipeSpawner pipeSpawner;
    public TextMeshProUGUI scoreText;
    public float startDelay = 2f;

    private int score;
    private float timeSinceLastScore;
    private float scoreInterval;
    private bool startDelayElapsed;

    public TextMeshProUGUI highScoreText;
    private int highScore;

    void Start()
    {
        score = 0;
        UpdateScoreText();
        timeSinceLastScore = 0f;
        scoreInterval = pipeSpawner.spawnInterval;
        startDelayElapsed = false;
        StartCoroutine(StartDelay());

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();

        birdController.onGameOver.AddListener(OnGameOver);

    }

    void Update()
    {
        if (birdController.IsGameOver()) return;

        if (birdController.HasStarted() && startDelayElapsed)
        {
            timeSinceLastScore += Time.deltaTime;

            if (timeSinceLastScore >= scoreInterval)
            {
                IncrementScore();
                timeSinceLastScore = 0f;
            }
        }
    }

    void IncrementScore()
    {
        score++;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void OnGameOver()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreText();
        }
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        startDelayElapsed = true;
    }

    void AddScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreText();
        }
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore;
    }
}
