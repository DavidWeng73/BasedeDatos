using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public GameObject Car;
    private int IndexPoint = 1;
    public Transform Point1;
    public Transform Point2;
    public Transform Point3;

    public float speed = 5f;
    private float playTime = 0f; 

    void Update()
    {
        playTime += Time.deltaTime; 

        if (Input.GetKeyDown(KeyCode.D))
        {
            CambiarPuntoDerecha();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            CambiarPuntoIzquierda();
        }

        MoverCoche();
        SobrevivienteAchievement();
        InmortalAchievement();
    }

    void CambiarPuntoDerecha()
    {
        if (IndexPoint < 2)
        {
            IndexPoint++;
        }
    }

    void CambiarPuntoIzquierda()
    {
        if (IndexPoint > 0)
        {
            IndexPoint--;
        }
    }

    void MoverCoche()
    {
        Transform targetPoint = null;

        if (IndexPoint == 0)
        {
            targetPoint = Point1;
        }
        else if (IndexPoint == 1)
        {
            targetPoint = Point2;
        }
        else if (IndexPoint == 2)
        {
            targetPoint = Point3;
        }

        if (targetPoint != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        }
    }

    public void OnGameOver()
    {
        Debug.Log("GAME OVER! Guardando puntuación...");
        string username = PlayerPrefs.GetString("CurrentUser", "Guest");
        float bestScore = PlayerPrefs.GetFloat(username + "_BestScore", 0f);

        if (playTime > bestScore) 
        {
            PlayerPrefs.SetFloat(username + "_BestScore", playTime);
            PlayerPrefs.Save();
            Debug.Log("Nueva mejor puntuación guardada para " + username + ": " + playTime);
        }
        else
        {
            Debug.Log("La puntuación no superó la mejor puntuación actual.");
        }

        UserManager userManager = FindObjectOfType<UserManager>();
        RankingManager rankingManager = FindObjectOfType<RankingManager>();

        if (userManager != null)
        {
            userManager.SaveUserScore(username, playTime);
        }
        else
        {
            Debug.LogError("UserManager no encontrado en la escena.");
        }

        if (rankingManager != null)
        {
            rankingManager.SaveScoreToCSV(username, playTime);
        }
        else
        {
            Debug.LogError("RankingManager no encontrado en la escena.");
        }

        SceneManager.LoadScene("Ranking");
    }

    void SobrevivienteAchievement()
    {
        if (playTime >= 10f)
        {
            AchievementsManager achievementsManager = FindObjectOfType<AchievementsManager>();
            if (achievementsManager != null)
            {
                achievementsManager.UnlockAchievement("Sobreviviente");
            }
            else
            {
                Debug.LogError("No se encontró `AchievementsManager` en la escena.");
            }
        }
    }

    void InmortalAchievement()
    {
        if (playTime >= 30f)
        {
            AchievementsManager achievementsManager = FindObjectOfType<AchievementsManager>();
            if (achievementsManager != null)
            {
                achievementsManager.UnlockAchievement("Inmortal");
            }
            else
            {
                Debug.LogError("No se encontró `AchievementsManager` en la escena.");
            }
        }
    }

}
