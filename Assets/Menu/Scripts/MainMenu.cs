using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
    public void GoToMainMenu()
    {
        Destroy(GameObject.Find("PlayerData"));
        Destroy(GameObject.Find("EnemyData"));
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
