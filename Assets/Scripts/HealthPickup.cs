using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    Player playerHealth;
    [SerializeField] int healthBonus = 50;
    [SerializeField] AudioClip healthPickUpSound;
    [SerializeField] [Range(0, 1)] float healthPickUpVolume = 0.75f;

    void Awake()
    {
        playerHealth = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerHealth.currentHealth<playerHealth.maxHealth)
        {
            AudioSource.PlayClipAtPoint(healthPickUpSound, Camera.main.transform.position, healthPickUpVolume);
            Destroy(gameObject);
            playerHealth.currentHealth = playerHealth.currentHealth + healthBonus;
            FindObjectOfType<HealthBar>().SetHealth(playerHealth.currentHealth);
        }
    }


}
