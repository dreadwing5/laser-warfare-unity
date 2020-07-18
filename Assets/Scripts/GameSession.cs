using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    Player player;
   [SerializeField]int score = 0;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {   
        if(FindObjectsOfType(GetType()).Length>1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);
        }


    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
        player.CheckForLevelUps(score);
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}

