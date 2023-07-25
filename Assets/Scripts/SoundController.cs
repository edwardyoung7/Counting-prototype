using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip goodSnackSound;
    public AudioClip badSnackSound;
    public AudioClip gameOverSound;
    public AudioClip buttonClick;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void ButtonClick()
    {
        DontDestroyOnLoad(gameObject);
        audioSource.PlayOneShot(buttonClick, 1.0f);
    }
}
