using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = .5f;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float projectileFireRate = 0.1f;
    [SerializeField] GameObject weapon;
    Camera gameCamera;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    Coroutine firingCoroutine;
   
    // Start is called before the first frame update
    void Start()
    {
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
            StopCoroutine(firingCoroutine);

    }

    IEnumerator FireCycle()
    {
        while(true)
        {
            GameObject laser =Instantiate(weapon, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
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
}
