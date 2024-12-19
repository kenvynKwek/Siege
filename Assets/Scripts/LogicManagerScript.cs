using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;

    void OnEnable()
    {
        PlayerHealth.zeroHealth += GameOver;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverUI.activeSelf && Input.GetKeyDown(KeyCode.Return)) RestartGame();
    }

    /// <summary>
    /// Freezes the game and displays the 'Game Over' message.
    /// </summary>
    void GameOver()
    {
        PlayerHealth.zeroHealth -= GameOver; // unsubscribe from event => so its called only once
        Time.timeScale = 0f; // freeze screen
        gameOverUI.SetActive(true); // display "game over" message
        Debug.Log("time has been frozen");
    }

    /// <summary>
    /// Unfreezes the scene & reloads the currently active scene.
    /// </summary>
    void RestartGame()
    {
        Time.timeScale = 1; // unfreeze
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reload scene
    }
}
