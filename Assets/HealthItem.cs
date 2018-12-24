using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
     [SerializeField] float healthRestored = 200f;

     [SerializeField] AudioClip pickupFX;
     
     private void OnTriggerEnter2D(Collider2D other)
    {
      
       RestoreHealth(other);

    }

    private void RestoreHealth(Collider2D other)
    {
        if(other.gameObject.layer == 8) //player layer ONLY
       {
           
           Player player = FindObjectOfType<Player>();
           AudioSource.PlayClipAtPoint(pickupFX, Camera.main.transform.position, 1f);
           float currentHealth = player.getHealth();
           float maxHealth = player.getMaxHealth();
           
           //Debug.Log("health now:" + currentHealth);
           
           if(currentHealth + healthRestored >= maxHealth)
             player.setHealth(maxHealth);
           else
             player.setHealth(currentHealth + healthRestored);
           
           //Debug.Log("health after" + player.getHealth());
           Destroy(gameObject);
       }
    }
}
