using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float slowMoTimeScale = 0.15f;
    private float gradualFreezeTiming = 5f;
    private GameObject player;
    private Coroutine gradualFreezeCoroutine;
    private float zoomInSize = 0.8f;
    private float zoomInTime = 2.5f;
    private float gameOverUIDelay = 4.5f;
    private float tintOverlayFadeTiming = 3f;

    public GameObject pauseUI;
    // game over variables
    private int killCount = 0;
    private float survivalTime = 0f;
    public GameObject gameOverUI;
    public Image gameOverOverlayTint;
    public TMPro.TextMeshProUGUI killsNumber;
    public TMPro.TextMeshProUGUI survivalTimeNumber;

    // play/pause gameplay variables
    private GameObject enemySpawner;

    void Awake()
    {
        player = GameObject.Find("Player");
        enemySpawner = GameObject.Find("Enemy Spawner");
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

        // update survival time
        if (!pauseUI.activeSelf)
        {
            survivalTime += Time.unscaledDeltaTime;
        }
    }

    private IEnumerator GameOverSequence()
    {
        player.GetComponent<PlayerMovement>().StopMovement(); // stop player movement
        player.GetComponent<PlayerHealth>().MakePlayerInvalid();

        Time.timeScale = slowMoTimeScale; // slow mo

        // zoom in camera pan to player
        CameraEffects.Instance.ZoomIn(zoomInSize, zoomInTime);

        // gradually freeze timescale
        gradualFreezeCoroutine = StartCoroutine(GradualFreeze(gradualFreezeTiming));

        // add gradual fade for this
        // display "game over" message

        // delay showing game over UI (let the camera pan & gradual freeze play out first)
        yield return new WaitForSecondsRealtime(gameOverUIDelay);
        gameOverUI.SetActive(true);

        // update stats
        killsNumber.text = $"{killCount}";
        int minutes = Mathf.FloorToInt(survivalTime / 60f);
        int seconds = Mathf.FloorToInt(survivalTime % 60);
        survivalTimeNumber.text = $"{minutes}m {seconds}s";

        // fade in tint overlay
        float elapsed = 0f;

        while (elapsed < tintOverlayFadeTiming)
        {
            Color colour = gameOverOverlayTint.color;
            float t = Mathf.Clamp01(elapsed / tintOverlayFadeTiming);
            colour.a = Mathf.Lerp(0, 1, t);
            gameOverOverlayTint.color = colour;

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// Plays the game over sequence.
    /// </summary>
    public void GameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    /// <summary>
    /// Unfreezes the scene & reloads the currently active scene.
    /// </summary>
    void RestartGame()
    {
        if (gradualFreezeCoroutine != null)
        {
            StopCoroutine(gradualFreezeCoroutine);
            gradualFreezeCoroutine = null;
        }

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

    /// <summary>
    /// Increment kill count.
    /// </summary>
    public void AddKillCount()
    {
        killCount++;
    }

    /// <summary>
    /// Play or pause the gameplay.
    /// </summary>
    /// <param name="gameplay">True or false to enable gameplay.</param>
    public void SetGameplay(bool gameplay)
    {
        enemySpawner.GetComponent<EnemySpawn>().SetCanSpawn(gameplay);
        // obstacle spawner
        // power up spawner
    }
}
