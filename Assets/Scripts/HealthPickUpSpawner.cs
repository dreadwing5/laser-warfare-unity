using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpSpawner : MonoBehaviour
{
    [SerializeField] GameObject healthBoost;
    [SerializeField] List<Transform> spawnPosition;
    [SerializeField] float spawnWait=2f;
    [SerializeField] float startWait = 1f;
    [SerializeField] bool looping = false;
    [SerializeField] float waitForNextSpawn = 30f;
    [SerializeField] int startingIndex = 0;
    int score;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            Debug.Log("Starting Coroutine");
            yield return StartCoroutine(HealthSpawner());

        }

        while (looping);
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    

    private IEnumerator HealthSpawner()
    {
        yield return new WaitForSeconds(startWait);
        {
            
                Debug.Log("Entering For Loop");
                for (int spawnIndex = startingIndex; spawnIndex < spawnPosition.Count; spawnIndex++)
                {

                    GameObject HealthBoost = Instantiate(healthBoost, spawnPosition[spawnIndex].transform.position, Quaternion.identity) as GameObject;
                    yield return new WaitForSeconds(spawnWait);
                    Destroy(HealthBoost);
                    yield return new WaitForSeconds(waitForNextSpawn);
                    Debug.Log("Inside For Loop");
                }
            Debug.Log("Exiting For Loop");
        }

    }
    
}
