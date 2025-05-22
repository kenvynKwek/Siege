using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pauseUI;

    void OnEnable()
    {

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
        // TODO: change to slow mo camera pan over to player + zoom in



        // freeze screen
        Time.timeScale = 0f;
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

    void PauseGame()
    {
        // freeze
        Time.timeScale = 0f;
        // display "pause" message
        pauseUI.SetActive(true);
    }

    void ResumeGame()
    {
        // unfreeze
        Time.timeScale = 1f;
        // set "pause" message to inactive
        pauseUI.SetActive(false);
    }
}
