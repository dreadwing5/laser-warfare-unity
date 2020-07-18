using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCleared : MonoBehaviour
{
    
    Text waveCleared;
    // Start is called before the first frame update
    void Start()
    {
        waveCleared = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateCurrentWave(int numWavesCleared)
    {
        waveCleared.text = "Wave Cleared : " + numWavesCleared;
    }
}