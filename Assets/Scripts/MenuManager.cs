using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public GameObject instructionScreen;
    public GameObject startScreen;
    public GameObject leaderBoardScreen;
    public TextMeshProUGUI highScore1;
    public TextMeshProUGUI highScore2;
    public TextMeshProUGUI highScore3;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLeaderBoard()
    {
        highScore1.text = MainManager.Instance.highScoreName1 + " " + MainManager.Instance.highScore1;
        highScore2.text = MainManager.Instance.highScoreName2 + " " + MainManager.Instance.highScore2;
        highScore3.text = MainManager.Instance.highScoreName3 + " " + MainManager.Instance.highScore3;
    }

    public void ClosePopUp()
    {
        instructionScreen.gameObject.SetActive(false);
        leaderBoardScreen.gameObject.SetActive(false);
        startScreen.gameObject.SetActive(true);
        MenuSound.Instance.ButtonClick();
    }

    public void HowToPlay()
    {
        instructionScreen.gameObject.SetActive(true);
        startScreen.gameObject.SetActive(false);
        MenuSound.Instance.ButtonClick();
    }

    public void LeaderBoard()
    {
        leaderBoardScreen.gameObject.SetActive(true);
        startScreen.gameObject.SetActive(false);
        UpdateLeaderBoard();
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

    public void SetPlayerName(string name)
    {
        MainManager.Instance.currentPlayer = name;
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
