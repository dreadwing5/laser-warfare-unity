using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] AudioClip powerUpPickUpSound;
    [SerializeField] [Range(0, 1)] float powerUpPickUpVolume;

    [SerializeField] public bool isPowerUp = false;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {

            isPowerUp = true;
            AudioSource.PlayClipAtPoint(powerUpPickUpSound, Camera.main.transform.position, powerUpPickUpVolume);
            Destroy(gameObject);
        }
    }
}
