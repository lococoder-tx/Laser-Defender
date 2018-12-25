using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreText;
    GameSession gameSession;

    public void Start()
    {
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
        scoreText.text =  gameSession.GetScore().ToString();
    }

    public void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }
}
