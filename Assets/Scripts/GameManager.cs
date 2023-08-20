using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText, livesText, highScoreText, playerNameText;
    [SerializeField] private GameObject gameOverScreen, player;
    private int score;
    private float snackSpawnRate;
    private float powerupSpawnRate;
    private int level;
    private int lives = 3;
    private AudioSource audioSource;
    
    public GameObject[] snacks;
    public GameObject[] powerUps;
    public bool isGameActive { get; private set; }

    // Start is called before the first frame update 
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        LevelDifficulty();
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        level = 1;
        snackSpawnRate = 2;
        ShowPlayer(true);
        SetHighScore();
        UpdateScore(0);
        PlayerName();
        UpdateLives(0);
        ShowUI(true);
        StartCoroutine(SpawnSnacks());
        StartCoroutine(SpawnPowerUps());
    }

    private void SetHighScore()
    {
        if (MainManager.Instance.highScoreName1 == null && MainManager.Instance.HighScore1 == 0)
        {
            highScoreText.text = "";
        }
        else
        {
            highScoreText.text = "High Score: " + MainManager.Instance.highScoreName1 + " " + MainManager.Instance.HighScore1;
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToAdd)
    {
        if (isGameActive)
        {
            lives -= livesToAdd;
            livesText.text = "Lives: " + lives;
            if (lives == 0)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        ShowGameOver(true);
        isGameActive = false;
        GameSound.Instance.GameOverSound();
        HighScoresRecord();
    }

    public void PlayerName()
    {
        playerNameText.text = "Player: " + MainManager.Instance.currentPlayer;
    }

    public void HighScoresRecord()
    {
        if (score > MainManager.Instance.HighScore1)
        {
            UpdateHighScore1();
        }

        else if (score < MainManager.Instance.HighScore1 && score > MainManager.Instance.HighScore2)   
        {
            UpdateHighScore2();
        }

        else if (score < MainManager.Instance.HighScore2 && score > MainManager.Instance.HighScore3)
        {
            UpdateHighScore3();
        }
    }

    public void LevelDifficulty()
    {
        switch (score)
        {
            case >= 20 and <= 100:
                level = 2;
                break;

            case >= 101 and <= 250:
                level = 3;
                break;

            case >= 251:
                level = 4;
                break;
        }
    }

    private void UpdateHighScore1()
    {
        MainManager.Instance.HighScore1 = score;
        MainManager.Instance.highScoreName1 = MainManager.Instance.currentPlayer;
        highScoreText.text = "High Score: " + MainManager.Instance.highScoreName1 + " " + MainManager.Instance.HighScore1;
        MainManager.Instance.SaveFile();
    }

    private void UpdateHighScore2()
    {
        MainManager.Instance.HighScore2 = score;
        MainManager.Instance.highScoreName2 = MainManager.Instance.currentPlayer;
        MainManager.Instance.SaveFile();
    }

    private void UpdateHighScore3()
    {
        MainManager.Instance.HighScore3 = score;
        MainManager.Instance.highScoreName3 = MainManager.Instance.currentPlayer;
        MainManager.Instance.SaveFile();
    }


    private void ShowPlayer(bool boolean)
    {
        player.gameObject.SetActive(boolean);
    }

    private void ShowGameOver(bool boolean)
    {
        gameOverScreen.gameObject.SetActive(boolean);
    }

    private void ShowUI(bool boolean)
    {
        scoreText.gameObject.SetActive(boolean);
        highScoreText.gameObject.SetActive(boolean);
        livesText.gameObject.SetActive(boolean);
        playerNameText.gameObject.SetActive(boolean);
    }

    IEnumerator SpawnSnacks()
    {
        while (isGameActive)
        {
            switch (level)
            {
                case 2:
                    snackSpawnRate = 1.5f;
                    break;

                case 3:
                    snackSpawnRate = 1f;
                    break;

                case 4:
                    snackSpawnRate = .75f;
                    break;
            }

            yield return new WaitForSeconds(snackSpawnRate);
            int index = Random.Range(0, snacks.Length);
            Instantiate(snacks[index]);
        }
    }

    IEnumerator SpawnPowerUps()
    {
        powerupSpawnRate = Random.Range(8, 15);
        while (isGameActive)
        {
            yield return new WaitForSeconds(powerupSpawnRate);
            int index = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[index]);
        }
    }
}
