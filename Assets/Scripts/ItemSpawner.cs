using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    
    [Header("Spawn Times")]
    
    [SerializeField] List<GameObject> Items;
    [SerializeField] float minTimeBetweenSpawn = 10f;
    [SerializeField] float maxTimeBetweenSpawn = 60f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    [SerializeField] float padding = .5f;
    Camera gameCamera;
    [SerializeField] float whenToSpawnItem;
    [SerializeField] bool itemPickedUp = true;
    
    void Start()
    {
        gameCamera = Camera.main;
        
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        
        //start off with a random time to spawn first item in gameWorld
        whenToSpawnItem = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        whenToSpawnItem -= Time.deltaTime;
        if(whenToSpawnItem <= 0 && itemPickedUp)
        {
            SpawnItem();
            whenToSpawnItem = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
        }
    }

    private void SpawnItem()
    {
        GameObject itemToSpawn = Items[Random.Range(0, Items.Count - 1)];
        float xSpawn = Random.Range(xMin, xMax);
        float ySpawn = Random.Range(yMin, yMax);
        Vector2 spawnPoint = new Vector2(xSpawn, ySpawn);
        
        //spawn item at random location in game world
        GameObject item = Instantiate(itemToSpawn, spawnPoint, Quaternion.identity);
        itemPickedUp = false;

    }

    public void PlayerPickedUpItem()
    {
        itemPickedUp = true;
    }
}
