using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    public void PrintPlayerScore(int score)
    {
      
        const string text = "Your score: ";
        scoreText.text = text + score;
    }


}
