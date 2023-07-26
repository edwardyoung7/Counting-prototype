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
        Debug.Log("Level: " + level + "Spawn: " + snackSpawnRate);

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

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        level = 1;
        snackSpawnRate = 2;
        StartCoroutine(SpawnSnacks());
        StartCoroutine(SpawnPowerUps());
        UpdateScore(0);
        UpdateLives(0);
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
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
                gameOverScreen.gameObject.SetActive(true);
                isGameActive = false;
                GameSound.Instance.GameOverSound();
            }
        }
    }

}
