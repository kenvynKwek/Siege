using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float slowMoTimeScale = 0.15f;
    private float gradualFreezeTiming = 5f;
    private GameObject player;

    public GameObject gameOverUI;
    public GameObject pauseUI;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverUI.activeSelf && Input.GetKeyDown(KeyCode.Return)) RestartGame();

        if (!gameOverUI.activeSelf)
        {
            if (!pauseUI.activeSelf && Input.GetKeyDown(KeyCode.Escape)) PauseGame();
            else if (pauseUI.activeSelf && Input.GetKeyDown(KeyCode.Escape)) ResumeGame();
        }
    }

    /// <summary>
    /// Freezes the game and displays the 'Game Over' message.
    /// </summary>
    public void GameOver()
    {
        player.GetComponent<PlayerMovement>().StopMovement(); // stop player movement
        player.GetComponent<PlayerHealth>().MakePlayerInvalid();

        Time.timeScale = slowMoTimeScale; // slow mo

        // zoom in camera pan to player

        // completely freeze after short delay
        StartCoroutine(GradualFreeze(gradualFreezeTiming));

        // add gradual fade for this
        // display "game over" message
        gameOverUI.SetActive(true);

        // show stats
    }

    /// <summary>
    /// Unfreezes the scene & reloads the currently active scene.
    /// </summary>
    void RestartGame()
    {
        // unfreeze
        Time.timeScale = 1f;
        // reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Freezes game & shows pause UI.
    /// </summary>
    void PauseGame()
    {
        // freeze
        Time.timeScale = 0f;
        // display "pause" message
        pauseUI.SetActive(true);
    }

    /// <summary>
    /// Unfreeze game & hide pause UI.
    /// </summary>
    void ResumeGame()
    {
        // unfreeze
        Time.timeScale = 1f;
        // set "pause" message to inactive
        pauseUI.SetActive(false);
    }

    /// <summary>
    /// Gradually freezes game.
    /// </summary>
    /// <param name="delay">The delay duration.</param>
    /// <returns>An IEnumerator for coroutine timing.</returns>
    private IEnumerator GradualFreeze(float delay)
    {
        float start = Time.timeScale;
        float elapsed = 0f;

        while (elapsed < delay)
        {
            elapsed += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.SmoothStep(start, 0f, elapsed / delay);
            yield return null;
        }

        Time.timeScale = 0f;
    }
}
