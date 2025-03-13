using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingUI : MonoBehaviour
{
    public RankingManager rankingManager;
    public Text rankingText; 

    void Start()
    {
        if (rankingManager == null)
        {
            rankingManager = FindObjectOfType<RankingManager>();
        }

        if (rankingManager != null)
        {
            List<string> topRanking = rankingManager.GetTopRanking();
            rankingText.text = "Ranking Global \n";

            int count = 1;
            foreach (string entry in topRanking)
            {
                string[] data = entry.Split(',');
                rankingText.text += count + ". " + data[0] + " - " + data[1] + " segundos\n";
                count++;
                if (count > 5) break;
            }
        }
        else
        {
            rankingText.text = "Error: No se encontró el RankingManager.";
        }
    }
}