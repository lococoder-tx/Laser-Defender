using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = .5f;
   

    [SerializeField] GameObject weapon;
    [SerializeField] float maxHealth = 1000;

    float health;

     [Header("Player Effect and Sound")]
    [SerializeField] GameObject onHitVFX;
    [SerializeField] AudioClip onHitSFX;
    [SerializeField] GameObject DestroyVFX;
    [SerializeField] AudioClip deathSound;
    [SerializeField] float deathVolume = 1f;
   
    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float projectileFireRate = 0.1f;
  
    Camera gameCamera;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    Coroutine firingCoroutine;
   
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        gameCamera = Camera.main;
        SetUpMoveBoundaries();  
    }



    // Update is called once per frame
    void Update()
    {
       
        Move();
        Fire();
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireCycle());
        }
        
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }

    }

    IEnumerator FireCycle()
    {
        while(true)   
        {
            GameObject laser =Instantiate(weapon, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            DamageDealer damageDealer = laser.GetComponent<DamageDealer>();
            AudioSource.PlayClipAtPoint(damageDealer.getShootFX(), Camera.main.transform.position, 
            damageDealer.getShootVolume());
            yield return new WaitForSeconds(projectileFireRate);
        }
    }
    private void SetUpMoveBoundaries()
    {
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        //when you multiply something by Time.deltaTime, game is frame-rate independent
        //also, we use viewport since it provides better support if we were to increase size of main camera
        
        
        //xpos
        var deltaX = (Input.GetAxis("Horizontal") * Time.deltaTime) * moveSpeed;
        var newPosX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

       //ypos
        var deltaY = (Input.GetAxis("Vertical") * Time.deltaTime) * moveSpeed;
        var newPosY = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newPosX, newPosY);
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
            FindObjectOfType<Level>().LoadGameOverScene();
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint(onHitSFX, Camera.main.transform.position, deathVolume);
            GameObject effect = Instantiate(onHitVFX, transform.position, Quaternion.identity);
            Destroy(effect,1f);
        }
    }

    private void Die()
    {
        
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
        Destroy(gameObject);
        GameObject effect = Instantiate(DestroyVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect,5f);
    }

    public float GetHealth()
    {
        if(health < 0)
            return 0;
        else return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetHealth(float amount)
    {
        health = amount;
    }
}
