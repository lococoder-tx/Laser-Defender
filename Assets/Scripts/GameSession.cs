using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score = 0;   
    [SerializeField] Player player;
     
    void Awake()
    {
        SetupSingelton();
        player = FindObjectOfType<Player>();
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

    public int GetScore()
    {
        return score;
    }

    public void addToScore(int score)
    {
        this.score += score;
    }

    public void removeFromScore(int score)
    {
        this.score -= score;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return player.GetHealth();
    }
}
