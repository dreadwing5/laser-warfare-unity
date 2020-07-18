using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipLevel : MonoBehaviour
{
    Text shipLevel;
    // Start is called before the first frame update
    void Start()
    {
        shipLevel = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateCurrentShipLevel(int currentLevel)
    {
        shipLevel.text = "Ship Level : " + currentLevel +  "/3"; 
    }
}
