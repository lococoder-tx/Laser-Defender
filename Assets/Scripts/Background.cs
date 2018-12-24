using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
   [SerializeField] float backgroundScrollSpeed = .5f;
   Material background;
   Vector2 offset;
    void Start()
    {
        background = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        background.mainTextureOffset += offset * Time.deltaTime;
    }
}
