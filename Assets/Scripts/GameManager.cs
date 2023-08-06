using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score;
    private float snackSpawnRate;
    private float powerupSpawnRate;
    private int level;
    private AudioSource audioSource;

    public int lives = 3;
    public GameObject[] snacks;
    public GameObject[] powerUps;
    public Text scoreText;
    public Text livesText;
    public Text highScoreText;
    public Text playerNameText;
    public bool isGameActive;
    public GameObject gameOverScreen;
    public GameObject player;


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

    private void SetHighScore()
    {
        if (MainManager.Instance.highScoreName1 == null && MainManager.Instance.highScore1 == 0)
        {
            highScoreText.text = "";
        }
        else
        {
            highScoreText.text = "High Score: " + MainManager.Instance.highScoreName1 + " " + MainManager.Instance.highScore1;
        }        
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        level = 1;
        snackSpawnRate = 2;
        SetHighScore();
        UpdateScore(0);
        PlayerName();
        UpdateLives(0);
        StartCoroutine(SpawnSnacks());
        StartCoroutine(SpawnPowerUps());
        player.gameObject.SetActive(true);
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

    public void UpdateScore(int scoreToAdd)
    {
        scoreText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToAdd)
    {
        livesText.gameObject.SetActive(true);
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
        gameOverScreen.gameObject.SetActive(true);
        isGameActive = false;
        GameSound.Instance.GameOverSound();
        HighScoresRecord();
    }

    public void PlayerName()
    {
        playerNameText.gameObject.SetActive(true);
        playerNameText.text = "Player: " + MainManager.Instance.currentPlayer;
    }

    public void HighScoresRecord()
    {
        if (score > MainManager.Instance.highScore1)
        {
            MainManager.Instance.highScore1 = score;
            MainManager.Instance.highScoreName1 = MainManager.Instance.currentPlayer;
            highScoreText.text = "High Score: " + MainManager.Instance.highScoreName1 + " " + MainManager.Instance.highScore1;
            MainManager.Instance.SaveFile();
        }

        else if (score < MainManager.Instance.highScore1 && score > MainManager.Instance.highScore2)   
        {
            MainManager.Instance.highScore2 = score;
            MainManager.Instance.highScoreName2 = MainManager.Instance.currentPlayer;
            MainManager.Instance.SaveFile();
        }

        else if (score < MainManager.Instance.highScore2 && score > MainManager.Instance.highScore3)
        {
            MainManager.Instance.highScore3 = score;
            MainManager.Instance.highScoreName3 = MainManager.Instance.currentPlayer;
            MainManager.Instance.SaveFile();
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
}
