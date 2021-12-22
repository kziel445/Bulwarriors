using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLose : MonoBehaviour
{
    [SerializeField] GameObject victory;
    [SerializeField] GameObject deafeat;

    [SerializeField] Transform playerBuildings;
    [SerializeField] Transform enemyBuildings;
    int playerBuildingsCount = 1;
    int enemyBuildingsCount = 1;
    private bool ifEndOfGame = false;

    void Update()
    {
        UpdatePlayerBuildings();
        if (playerBuildingsCount == 0 && !ifEndOfGame)
        {
            DefeatScreen();
            ifEndOfGame = true;
        }
        UpdateEnemyBuildings();
        if (enemyBuildingsCount == 0 && !ifEndOfGame)
        {
            VictoryScreen();
            ifEndOfGame = true;
        }
    }
    public int UpdatePlayerBuildings()
    {
        playerBuildingsCount = 0;
        foreach (Transform child in playerBuildings)
        {
            playerBuildingsCount += child.childCount;
        }
        return playerBuildingsCount;
    }
    public int UpdateEnemyBuildings()
    {
        enemyBuildingsCount = 0;
        foreach (Transform child in enemyBuildings)
        {
            enemyBuildingsCount += child.childCount;
        }
        return enemyBuildingsCount;
    }
    public void VictoryScreen()
    {
        GetStatistics(victory.transform);
        victory.SetActive(true);
        Debug.Log("You are a winner! :D");
        
    }
    public void DefeatScreen()
    {
        GetStatistics(deafeat.transform);
        deafeat.SetActive(true);
        Debug.Log("You lose");
    }
    public void GetStatistics(Transform screen)
    {
        string[] values = new string[] 
        {
            GameObject.Find("PlayerData").GetComponent<Statistics.Data>().moneyCollected.ToString(),
            GameObject.Find("PlayerStatistics").GetComponent<Statistics.Statistics>().TimerString(),
            GameObject.Find("PlayerData").GetComponent<Statistics.Data>().unitsRecruted.ToString()
        };
        string[] lines = screen.Find("Statistics").GetComponent<TMPro.TextMeshProUGUI>().text.Split('\n');
        for(int i = 0; i < lines.Length; i++)
        {
            lines[i]+=values[i];
        }
        screen.Find("Statistics").GetComponent<TMPro.TextMeshProUGUI>().text = string.Join("\n", lines);
    }
}
