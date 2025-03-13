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
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI bestScoreText;

    void Start()
    {
        UpdateUI();
    }

    void OnEnable() 
    {
        UpdateUI();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Ranking()
    {
        SceneManager.LoadScene("Ranking");
    }
    public void Achievements()
    {
        SceneManager.LoadScene("AchievementsScene");
    }

    public void Search()
    {
        SceneManager.LoadScene("SearchScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void UpdateUI()
    {
        string username = PlayerPrefs.GetString("CurrentUser", "Guest");
        float bestScore = PlayerPrefs.GetFloat(username + "_BestScore", 0f);

        usernameText.text = "Usuario: " + username;
        bestScoreText.text = "Mejor Puntuación: " + bestScore.ToString("F2") + " segundos";

        Debug.Log("Nombre cargado: " + username);
        Debug.Log("Mejor puntuación cargada: " + bestScore);
    }
}
