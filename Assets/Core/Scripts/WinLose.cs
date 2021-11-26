using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLose : MonoBehaviour
{   
    [SerializeField]
    Transform playerBuildings;
    [SerializeField]
    Transform enemyBuildings;
    int playerBuildingsCount = 1;
    int enemyBuildingsCount = 1;
    private bool ifWin, ifEndOfGame = false;

    void Update()
    {
        UpdatePlayerBuildings();
        if (playerBuildingsCount == 0) LoseScreen();
        UpdateEnemyBuildings();
        if (enemyBuildingsCount == 0) WinScreen();
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
    public void WinScreen()
    {
        
        Debug.Log("You are a winner! :D");
    }
    public void LoseScreen()
    {
        Debug.Log("You lose");
    }
}
