using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour
{
    public static GameSound Instance;
    [SerializeField] private AudioClip goodSnackSound, badSnackSound, gameOverSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GoodSnackSound()
    {
        audioSource.PlayOneShot(goodSnackSound, 1.0f);
    }

    public void BadSnackSound()
    {
        audioSource.PlayOneShot(badSnackSound, 1.0f);
    }

    public void GameOverSound()
    {
        audioSource.PlayOneShot(gameOverSound, 1.0f);
    }

}
