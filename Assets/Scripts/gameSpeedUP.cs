using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSpeedUP : MonoBehaviour
{
    GameSession gameSession;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        

    }

    // Update is called once per frame
    void Update()
    {
        score = gameSession.GetScore();
        if (score > 1500)
        {
            Time.timeScale += 0.001f * Time.deltaTime;
        }

    }


}
