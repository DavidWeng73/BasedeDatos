using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private TextMeshProUGUI Btn_StartGame;
    private TextMeshProUGUI Btn_Settings;
    private TextMeshProUGUI Btn_Exit;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
