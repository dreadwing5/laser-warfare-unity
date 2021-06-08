using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Configuration parameters

    [Header("Player")]


    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] public float currentHealth = 500f;
    [SerializeField] public float maxHealth = 500f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip playerHitSound;
    [SerializeField] [Range(0, 1)] float playerHitSOundVolume;

    [Header("Projectile")]

    [SerializeField] public float projectileSpeed = 10f;
    [SerializeField] public float projectileFiringRate = 0.1f;
    [SerializeField] public GameObject laserPrefab;


    [Header("Power Ups")]

    [SerializeField] GameObject levelUpVFX;
    [SerializeField] AudioClip levelUpSFX;
    [SerializeField] int[] levelUpScoreRequired;
    [SerializeField] int currentLevel;
    [SerializeField] [Range(0, 1)] float levelUpSFXVolume;
    [SerializeField] int levelUpHealthBonus = 50;
    [SerializeField] float levelUpFiringRateBonus = -0.005f;
    [SerializeField] int projectileSpeedBonus = 5;
    [SerializeField] Sprite[] playerShipArray;
    [SerializeField] GameObject[] sideGuns;
    [SerializeField] float maxPowerUpDuration = 30f;
    ShipLevel shiplevel;
    float reloadTime = 0.5f;
    public float nextFireTime = 0.0f;
    public float nextFire = 1.5F;

    [SerializeField] PowerUp powerUp;



    Coroutine firingCoroutine;


    float minX;
    float maxX;
    float minY;
    float maxY;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        FindObjectOfType<HealthBar>().SetMaxHealth(maxHealth);
        SetUpMoveBoundaries();
        shiplevel = FindObjectOfType<ShipLevel>();
        // Debug.Log(powerUp.isPowerUp);

    }



    // Update is called once per frame
    void Update()
    {
        Move();

        //Auto fire for Mobile devices
        if (Time.time > nextFire)
        {
            nextFire = Time.time + projectileFiringRate;


            Shoot();



        }


    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        currentHealth -= damageDealer.GetDamage();
        AudioSource.PlayClipAtPoint(playerHitSound, Camera.main.transform.position, playerHitSOundVolume);
        damageDealer.Hit();
        FindObjectOfType<HealthBar>().SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {


        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        FindObjectOfType<Level>().LoadLastScene();

    }


    //Fire control, we need to change this in order to get it working for android
    //Touch down->fire continuosly with delay so that prefab doesn't overlap
    //Touch up -> stop firing

    // public void Fire()
    // {
    //     if (Input.GetButtonDown("Fire1"))
    //     {
    //         firingCoroutine = StartCoroutine(FireContinuously());
    //     }
    //     if (Input.GetButtonUp("Fire1"))
    //     {
    //         StopCoroutine(firingCoroutine);
    //     }


    // }
    /*  IEnumerator FireContinuously()
     {
         while (true)
         {


             GameObject laser = Instantiate(laserPrefab,
                     transform.position,
                     Quaternion.identity) as GameObject;
             laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
             yield return new WaitForSeconds(projectileFiringRate);
         }
     } */

    public void Shoot()
    {
        if (powerUp.isPowerUp)
        {
            Debug.Log("Activate Side Guns");
            ActivateSideGuns();

        }

        GameObject laser = Instantiate(laserPrefab,
            transform.position,
            Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);




    }


    // //Player Move Controller

    private void Move()
    {

        // Touch Controls 
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        gameObject.transform.position = mousePosition;


        /* var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);
        transform.position = new Vector3(newXPos, newYPos, transform.position.z); */
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    public void RecoverHealth(int recoveryBonus)
    {

        currentHealth += recoveryBonus;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void CheckForLevelUps(int score)
    {
        int maxLevel = levelUpScoreRequired.Length;
        if (currentLevel != maxLevel && score >= levelUpScoreRequired[currentLevel])
        {
            HandleLevelUps();
        }
        else if (currentLevel == maxLevel)
        {
            Debug.Log("Max Level Reached");
        }

    }

    public void HandleLevelUps()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = playerShipArray[currentLevel];
        currentLevel++;
        GameObject levelUpeffect = Instantiate(levelUpVFX, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(levelUpSFX, Camera.main.transform.position, levelUpSFXVolume);
        shiplevel.UpdateCurrentShipLevel(currentLevel);

        maxHealth += levelUpHealthBonus;
        currentHealth = maxHealth;
        projectileFiringRate += levelUpFiringRateBonus;
        projectileSpeed += projectileSpeedBonus;
        FindObjectOfType<HealthBar>().SetMaxHealth(maxHealth);

    }

    public void ActivateSideGuns()
    {


        foreach (GameObject guns in sideGuns)
        {
            guns.SetActive(true);
            Debug.Log("Setting Active");
        }
        StartCoroutine(WaitAndSetActive());
    }

    IEnumerator WaitAndSetActive()
    {

        yield return new WaitForSeconds(maxPowerUpDuration);
        Debug.Log("Power Up Duration is : " + maxPowerUpDuration);
        powerUp.isPowerUp = false;
        foreach (GameObject guns in sideGuns)
        {
            guns.SetActive(false);
        }

    }
}

