using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumStatistics : MonoBehaviour
{
    private void Start() 
    {
        GetStatistics();
        GetGameState();
        /* if(GameObject.Find("PlayerData")!= null)
        {
            GetStatistics();
            GetGameState();
        } */
    }   
    public void GetStatistics()
    {
        Statistics.Data playerData = GameObject.Find("PlayerData").GetComponent<Statistics.Data>();
        string[] values = new string[] 
        {
            playerData.moneyCollected.ToString(),
            playerData.TimerString(playerData.timer),
            playerData.unitsRecruted.ToString()
        };
        string[] lines = gameObject.transform.Find("Statistics").GetComponent<TMPro.TextMeshProUGUI>().text.Split('\n');
        for(int i = 0; i < lines.Length; i++)
        {
            lines[i]+=values[i];
        }
        gameObject.transform.Find("Statistics").GetComponent<TMPro.TextMeshProUGUI>().text = string.Join("\n", lines);
    }
    public void GetGameState()
    {
        if(GameObject.Find("PlayerData").GetComponent<Statistics.Data>().isVictory)
        {
            gameObject.transform.Find("State").GetComponent<TMPro.TextMeshProUGUI>().text = "Victory!";
        }
        else
        {
            gameObject.transform.Find("State").GetComponent<TMPro.TextMeshProUGUI>().text = "Defeat";
            gameObject.transform.Find("State").GetComponent<TMPro.TextMeshProUGUI>().color = Color.black;
        }
    }
}
