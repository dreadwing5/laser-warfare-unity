using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AesteroidSpawner : MonoBehaviour
{
    [SerializeField] GameObject aesteroidPrefab;
    [SerializeField] float respawnTime = 100f;
    private Vector2 screenBound;
    // Start is called before the first frame update
    void Start()
    {
        screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(AesteroidWave());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator AesteroidWave()

    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }

        
    }


    public void spawnEnemy()
    {
        GameObject aesteroid = Instantiate(aesteroidPrefab) as GameObject;
        aesteroid.transform.position = new Vector2(Random.Range(-screenBound.x, screenBound.x), screenBound.y * 2);

    }

}

  
