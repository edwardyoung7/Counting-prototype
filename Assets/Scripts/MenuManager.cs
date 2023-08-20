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
    [SerializeField] private GameObject instructionScreen, startScreen, leaderBoardScreen;
    [SerializeField] private TextMeshProUGUI highScore1, highScore2, highScore3;


    public void SetPlayerName(string name)
    {
        MainManager.Instance.currentPlayer = name;
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
        MenuSound.Instance.ButtonClick();
    }

    public void HowToPlay()
    {
        ShowInstructionScreen(true);
        ShowStartScreen(false);
        MenuSound.Instance.ButtonClick();
    }

    public void LeaderBoard()
    {
        ShowLeadBoard(true);
        ShowStartScreen(false);
        UpdateLeaderBoard();
        MenuSound.Instance.ButtonClick();
    }

    public void ClosePopUp()
    {
        ShowInstructionScreen(false);
        ShowLeadBoard(false);
        ShowStartScreen(true);
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

    private void UpdateLeaderBoard()
    {
        highScore1.text = MainManager.Instance.highScoreName1 + " " + MainManager.Instance.HighScore1;
        highScore2.text = MainManager.Instance.highScoreName2 + " " + MainManager.Instance.HighScore2;
        highScore3.text = MainManager.Instance.highScoreName3 + " " + MainManager.Instance.HighScore3;
    }

    private void ShowStartScreen(bool boolean)
    {
        startScreen.gameObject.SetActive(boolean);
    }

    private void ShowInstructionScreen(bool boolean)
    {
        instructionScreen.gameObject.SetActive(boolean);
    }

    private void ShowLeadBoard(bool boolean)
    {
        leaderBoardScreen.gameObject.SetActive(boolean);
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
