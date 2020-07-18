using UnityEngine;

public class HealthPack : MonoBehaviour
{
    Player playerHealth;
  
    [SerializeField] AudioClip healthPickUpSound;
    [SerializeField] [Range(0, 1)] float healthPickUpVolume = 0.75f;
    [SerializeField] Sprite[] healthPackType;
    [SerializeField][Range(1,100)] int percentChanceBlue, percentChanceRed;
    [SerializeField] int greenRecoveryBonus, blueRecoveryBonus, redRecoveryBonus;
    private int recoveryBonus;

    void Start()
    {
        RandomizeHealthPacksType();
        playerHealth = FindObjectOfType<Player>();
    }

    private void RandomizeHealthPacksType()
    {
        int roll = Random.Range(1, 101);
        if (roll <= percentChanceRed)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = healthPackType[1];
            recoveryBonus = redRecoveryBonus;
            return;

        }
        if (roll <= percentChanceBlue)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = healthPackType[0];
            recoveryBonus = blueRecoveryBonus;
            return;

        }

        recoveryBonus = greenRecoveryBonus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            
            AudioSource.PlayClipAtPoint(healthPickUpSound, Camera.main.transform.position, healthPickUpVolume);
            Destroy(gameObject);
            playerHealth.RecoverHealth(recoveryBonus);
            FindObjectOfType<HealthBar>().SetHealth(playerHealth.currentHealth);
        }
    }

}
