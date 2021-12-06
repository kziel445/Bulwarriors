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
        victory.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text +=
            GameObject.Find("PlayerStatistics").GetComponent<Statistics.Statistics>().moneyCollected.ToString();
        victory.SetActive(true);
        Debug.Log("You are a winner! :D");
        
    }
    public void DefeatScreen()
    {
        deafeat.transform.Find("Statistics").GetComponent<TMPro.TextMeshProUGUI>().text +=
            GameObject.Find("PlayerStatistics").GetComponent<Statistics.Statistics>().moneyCollected.ToString();
        deafeat.SetActive(true);
        Debug.Log("You lose");
    }
}
