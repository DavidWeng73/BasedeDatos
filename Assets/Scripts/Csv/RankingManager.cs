using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RankingManager : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/ranking.csv";

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Username,Score\n"); 
        }
    }

    public void SaveScoreToCSV(string username, float score)
    {
        string newEntry = username + "," + score.ToString("F2") + "\n";
        File.AppendAllText(filePath, newEntry);
        Debug.Log("Puntuación guardada en ranking.csv: " + newEntry);
    }

    public List<string> GetTopRanking()
    {
        List<string> rankings = new List<string>();

        if (!File.Exists(filePath)) return rankings;

        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++) 
        {
            rankings.Add(lines[i]);
        }

        rankings.Sort((a, b) => float.Parse(b.Split(',')[1]).CompareTo(float.Parse(a.Split(',')[1])));

        return rankings;
    }
}
