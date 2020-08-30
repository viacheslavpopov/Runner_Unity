using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private int difficultyLevel = 1;
    [SerializeField] private int maxDifficultyLevel = 10;
    [SerializeField] private int scoreToNextLevel = 10;
    TextMeshProUGUI scoreText;

    public float PlayerScore { get; set; }

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerScore >= scoreToNextLevel)
        {
            LevelUp();
        }
        PlayerScore += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)PlayerScore).ToString();
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
