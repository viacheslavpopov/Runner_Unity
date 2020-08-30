using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject activeUI;
    [SerializeField] GameObject deathUI;


    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        deathUI.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsDead)
        {
            Die();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Die()
    {
        activeUI.SetActive(false);

        deathUI.SetActive(true);
        Score score = FindObjectOfType<Score>();
        int playerScore = (int)score.PlayerScore; 
        deathUI.GetComponent<DeathScreen>().PrintPlayerScore(playerScore);
        Time.timeScale = 0;
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
