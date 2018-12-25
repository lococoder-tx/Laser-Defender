using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthDisplay : MonoBehaviour
{
    Text healthText;
    GameSession gameSession;

    public void Start()
    {
        healthText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }

    public void Update()
    {
        
        healthText.text = gameSession.GetHealth().ToString();
    }
}
