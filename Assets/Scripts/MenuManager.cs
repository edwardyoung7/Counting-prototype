using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public GameObject instructionScreen;
    public GameObject startScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePopUp()
    {
        instructionScreen.gameObject.SetActive(false);
        startScreen.gameObject.SetActive(true);
        MenuSound.Instance.ButtonClick();
    }

    public void HowToPlay()
    {
        instructionScreen.gameObject.SetActive(true);
        startScreen.gameObject.SetActive(false);
        MenuSound.Instance.ButtonClick();
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
        MenuSound.Instance.ButtonClick();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        MenuSound.Instance.ButtonClick();
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
        MenuSound.Instance.ButtonClick();
    }

    public void Exit()
    {
        MenuSound.Instance.ButtonClick();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
    }
}
