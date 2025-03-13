using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AchievementsUI : MonoBehaviour
{
    public Transform contentPanel; // Panel donde se mostrarán los logros
    private AchievementsManager achievementsManager;

    void Start()
    {
        achievementsManager = FindObjectOfType<AchievementsManager>();

        if (achievementsManager == null)
        {
            Debug.LogError("No se encontró `AchievementsManager` en la escena.");
            return;
        }

        if (contentPanel == null)
        {
            Debug.LogError("`contentPanel` no está asignado en el Inspector.");
            return;
        }

        LoadAchievements();
    }

    void LoadAchievements()
    {
        AchievementsManager.AchievementList achievements = achievementsManager.LoadAchievements();

        if (achievements == null || achievements.achievements.Count == 0)
        {
            Debug.LogWarning("No hay logros en `logros.xml`.");
            return;
        }

        Debug.Log($"Generando {achievements.achievements.Count} logros en la UI.");

        foreach (var achievement in achievements.achievements)
        {
            GameObject newAchievement = new GameObject(achievement.name);
            newAchievement.transform.SetParent(contentPanel, false);

            TextMeshProUGUI nameText = newAchievement.AddComponent<TextMeshProUGUI>();
            nameText.text = achievement.name + " - " + (achievement.unlocked ? "Obtenido" : "No obtenido");
            nameText.fontSize = 24;
            nameText.color = Color.white;
            nameText.alignment = TextAlignmentOptions.Center;

            Debug.Log($"Logro agregado a la UI: {achievement.name} - Estado: {(achievement.unlocked ? "Obtenido" : "No obtenido")}");
        }
    }



public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
