using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private float score;

    private int difficultyLevel = 1;
    [SerializeField] private int maxDifficultyLevel = 10;
    [SerializeField] private int scoreToNextLevel = 10;
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(score >= scoreToNextLevel)
        {
            LevelUp();
        }
        score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)score).ToString();
    }

     private void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
        {
            return;
        }
        scoreToNextLevel *= 2;
        difficultyLevel++;
        FindObjectOfType<Player>().IncreasePlayerSpeed();
        Debug.Log("Level Up!");

    }
}
