using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideGun : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        
    }
    public void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopAllCoroutines();
        }


    }
    IEnumerator FireContinuously()
    {
        while (true)
        {


            GameObject laser = Instantiate(player.laserPrefab,
                    transform.position,
                    Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.projectileSpeed);
            yield return new WaitForSeconds(player.projectileFiringRate);
        }
    }
}
