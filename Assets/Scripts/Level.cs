using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [SerializeField] float gameOverDelay = 2f;

    public void LoadGameScene()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("Game");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
        
    }

    public void LoadLastScene()
    {
        StartCoroutine(WaitAndLoad());
        
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("Game Over");
    }
}
