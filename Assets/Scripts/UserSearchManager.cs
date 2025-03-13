using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using static AchievementsManager;

public class UserSearchManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI registerDateText;
    public TextMeshProUGUI achievementsText;

    private string usersFilePath;
    private string achievementsFilePath;

    void Start()
    {
        usersFilePath = Application.persistentDataPath + "/users.json";
        achievementsFilePath = Application.persistentDataPath + "/logros.xml";

        Debug.Log("Cargando usuarios desde: " + usersFilePath);
    }

    public void SearchUser()
    {
        string username = usernameInput.text.Trim();
        if (string.IsNullOrEmpty(username))
        {
            Debug.Log("Introduce un nombre de usuario.");
            return;
        }

        UserDataBase dataBase = LoadUserData();
        User foundUser = dataBase.users.Find(user => user.username == username);

        if (foundUser != null)
        {
            usernameText.text = "Usuario: " + foundUser.username;
            bestScoreText.text = "Puntuación Máxima: " + PlayerPrefs.GetFloat(foundUser.username + "_BestScore", 0f).ToString("F2");
            registerDateText.text = "Fecha de Registro: " + PlayerPrefs.GetString(foundUser.username + "_RegisterDate", "Desconocida");

            string achievements = GetUserAchievements(foundUser.username);
            achievementsText.text = "Logros Obtenidos: " + achievements;

            Debug.Log("Usuario encontrado: " + foundUser.username);
        }
        else
        {
            usernameText.text = "Usuario: No encontrado";
            bestScoreText.text = "Puntuación Máxima: -";
            registerDateText.text = "Fecha de Registro: -";
            achievementsText.text = "Logros Obtenidos: -";
            Debug.Log("Usuario no encontrado.");
        }
    }

    private UserDataBase LoadUserData()
    {
        if (!File.Exists(usersFilePath))
        {
            Debug.LogWarning("`users.json` no encontrado en: " + usersFilePath);
            return new UserDataBase();
        }

        string json = File.ReadAllText(usersFilePath);
        Debug.Log("`users.json` cargado: " + json);
        return JsonUtility.FromJson<UserDataBase>(json) ?? new UserDataBase();
    }

    private string GetUserAchievements(string username)
    {
        if (!File.Exists(achievementsFilePath))
        {
            return "Ninguno";
        }

        XmlSerializer serializer = new XmlSerializer(typeof(AchievementList));
        using (StreamReader reader = new StreamReader(achievementsFilePath))
        {
            AchievementList achievements = (AchievementList)serializer.Deserialize(reader);
            List<string> unlocked = new List<string>();

            foreach (var achievement in achievements.achievements)
            {
                if (achievement.unlocked)
                {
                    unlocked.Add(achievement.name);
                }
            }

            return unlocked.Count > 0 ? string.Join(", ", unlocked) : "Ninguno";
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}