using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

   [SerializeField] int damage = 100;

   [SerializeField] AudioClip shootFX;

   [SerializeField] float shootSoundVolume = .5f;

    public void Hit()
    {
        Destroy(gameObject);
    }

    public int getDamage()
    {
        return damage;
    }

    public AudioClip getShootFX()
    {
        return shootFX;
    }

    public float getShootVolume()
    {
        return shootSoundVolume;
    }
}
