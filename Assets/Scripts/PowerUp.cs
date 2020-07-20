using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    Player player;
    [SerializeField] AudioClip powerUpPickUpSound;
    [SerializeField] [Range(0, 1)] float powerUpPickUpVolume;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {

            player.ActivateSideGuns();
            AudioSource.PlayClipAtPoint(powerUpPickUpSound, Camera.main.transform.position, powerUpPickUpVolume);
            Destroy(gameObject);
        }
    }
}
