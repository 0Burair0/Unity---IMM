using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI keyText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameWonText;
    public Button restartButton;
    public bool isGameActive;
    public int keys;
    public int lives;
    // Start is called before the first frame update
    void Start()
    {
        keys = 0;
        lives = 3;
        keyText.text = "Keys: " + 0;
        livesText.text = "Lives: " + 3;
        isGameActive = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateKeys(int keysToAdd)
    {
        keys += keysToAdd;
        keyText.text = "Keys: " + keys;
    }

    public void UpdateLives(int livesToMinus)
    {
        lives = lives -  livesToMinus;
        livesText.text = "Lives: " + lives;
        if (lives == 0){
            GameOver();
        }
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
    }

    public void GameWon()
    {
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
        gameWonText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
   
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
