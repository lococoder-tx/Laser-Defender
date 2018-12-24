using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     [Header("Enemy Stats")]
    [SerializeField] public float health = 100;
    [SerializeField] float minTimeBetweenShots = .2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Enemy Components")]
    
    [SerializeField] AudioClip deathSound; //played upon the death of an enemy
    [Range(0,1)] [SerializeField] float deathVolume = 0.7f; //0-1 range

    [SerializeField] GameObject weapon;
    [SerializeField] GameObject DestroyVFX;
    [SerializeField] GameObject onHitVFX;

     float shotCounter;
    float timeSinceLastShot = 0;
    
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Update()
    {
        CountdownAndShoot();
    }

    private void CountdownAndShoot()
    {
        timeSinceLastShot += Time.deltaTime;
        if(timeSinceLastShot >= shotCounter)
        {
            Fire();
            timeSinceLastShot = 0;
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject Enemylaser = Instantiate(weapon, transform.position, Quaternion.identity) as GameObject;
        Enemylaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        
        //play audio fx for that specific weapon type
        DamageDealer damageDealer = Enemylaser.GetComponent<DamageDealer>();
        AudioSource.PlayClipAtPoint(damageDealer.getShootFX(), Camera.main.transform.position, 
        damageDealer.getShootVolume());
    
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(damageDealer != null)
            ProcessHit(damageDealer);
    
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamage();
        damageDealer.Hit();
        if(health <= 0)
        {  
            Die();
        }
        else
        {
            GameObject effect = Instantiate(onHitVFX, transform.position, Quaternion.identity);
            Destroy(effect,1f);
        }
        
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
        Destroy(gameObject);
        GameObject effect = Instantiate(DestroyVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect,1f);
    }


}
