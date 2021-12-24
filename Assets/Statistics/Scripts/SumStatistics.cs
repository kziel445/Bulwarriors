using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumStatistics : MonoBehaviour
{
    private void Start() {
        if(GameObject.Find("PlayerData")!= null)
            GetStatistics();
    }   
    public void GetStatistics()
    {
        string[] values = new string[] 
        {
            GameObject.Find("PlayerData").GetComponent<Statistics.Data>().moneyCollected.ToString(),
            GameObject.Find("PlayerData").GetComponent<Statistics.Data>().TimerString(),
            GameObject.Find("PlayerData").GetComponent<Statistics.Data>().unitsRecruted.ToString()
        };
        string[] lines = gameObject.transform.Find("Statistics").GetComponent<TMPro.TextMeshProUGUI>().text.Split('\n');
        for(int i = 0; i < lines.Length; i++)
        {
            lines[i]+=values[i];
        }
        gameObject.transform.Find("Statistics").GetComponent<TMPro.TextMeshProUGUI>().text = string.Join("\n", lines);
    }
}
