using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    [Serializable]
    public class Achievement
    {
        public string name;
        public string description;
        public bool unlocked;
    }

    [Serializable]
    public class AchievementList
    {
        public List<Achievement> achievements = new List<Achievement>();
    }

    private string filePath;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/logros.xml";
        Debug.Log("Cargando logros desde: " + filePath);
        LoadAchievements();
    }

    public void SaveAchievements(AchievementList achievements)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AchievementList));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, achievements);
            }
            Debug.Log("Logros guardados en: " + filePath);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error al guardar logros: " + ex.Message);
        }
    }

    public AchievementList LoadAchievements()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("No se encontró `logros.xml`, creando archivo nuevo...");
            AchievementList defaultAchievements = new AchievementList();
            defaultAchievements.achievements = new List<Achievement>
        {
            new Achievement { name = "Sobreviviente", description = "Sobrevive por más de 10 segundos", unlocked = false },
            new Achievement { name = "Inmortal", description = "Sobrevive por más de 30 segundos", unlocked = false }
        };

            SaveAchievements(defaultAchievements);
            return defaultAchievements;
        }

        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AchievementList));
            using (StreamReader reader = new StreamReader(filePath))
            {
                AchievementList achievements = (AchievementList)serializer.Deserialize(reader);
                Debug.Log($"Logros cargados correctamente desde `logros.xml`. Cantidad: {achievements.achievements.Count}");

                foreach (var achievement in achievements.achievements)
                {
                    Debug.Log($"Logro cargado: {achievement.name} - Estado: {(achievement.unlocked ? "Obtenido" : "No obtenido")}");
                }

                return achievements;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error al leer `logros.xml`: " + ex.Message);
            return new AchievementList();
        }
    }

    public void UnlockAchievement(string achievementName)
    {
        AchievementList achievements = LoadAchievements();
        foreach (Achievement achievement in achievements.achievements)
        {
            if (achievement.name == achievementName && !achievement.unlocked)
            {
                achievement.unlocked = true;
                Debug.Log("Logro desbloqueado: " + achievement.name);
                SaveAchievements(achievements);
                return;
            }
        }
    }
}
