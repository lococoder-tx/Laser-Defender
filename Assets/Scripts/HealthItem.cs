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
           
           FindObjectOfType<ItemSpawner>().PlayerPickedUpItem();
           Player player = FindObjectOfType<Player>();
           AudioSource.PlayClipAtPoint(pickupFX, Camera.main.transform.position, 1f);
           float currentHealth = player.GetHealth();
           float maxHealth = player.GetMaxHealth();
           
           //Debug.Log("health now:" + currentHealth);
           
           if(currentHealth + healthRestored >= maxHealth)
             player.SetHealth(maxHealth);
           else
             player.SetHealth(currentHealth + healthRestored);
           
           //Debug.Log("health after" + player.getHealth());
           Destroy(gameObject);
       }
    }
}
