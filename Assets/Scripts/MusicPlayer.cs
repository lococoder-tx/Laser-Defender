using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SetupSingelton();
    }

    private void SetupSingelton()
    {
        if(FindObjectsOfType(GetType()).Length > 1) //getType() returns the type of the gameOBject i.e musicplayer
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}