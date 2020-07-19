using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aesteroid : MonoBehaviour
{
    [SerializeField] int Health = 500;
    [SerializeField] GameObject deathVFX;
    [SerializeField] [Range(0,1)] float deathSoundVolume;
    [SerializeField] AudioClip deathSound;
    [SerializeField] float durationOfExplosion;
    [SerializeField] int points;
    [SerializeField] float moveSpeed = 10f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)

    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHits(damageDealer);

    }

    private void ProcessHits(DamageDealer damageDealer)
    {
        Health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(points);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        Destroy(explosion, durationOfExplosion);
    }


}
