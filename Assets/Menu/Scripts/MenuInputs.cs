using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInputs : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //if (Time.timeScale == 0) Time.timeScale = 1;
            //else Time.timeScale = 0;
            SceneManager.LoadScene(0);
        }
    }
}
