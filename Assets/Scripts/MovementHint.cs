using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHint : MonoBehaviour
{
    private bool hasMoved = false;
    private GameObject gameManager;
    private SpriteRenderer[] arrowKeys;
    private float fadeOutDuration = 3f;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager.GetComponent<GameManager>().SetGameplay(false);
        arrowKeys = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasMoved) return; // do nothing

        // detect movement
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            hasMoved = true;
            StartCoroutine(FadeArrowKeyHint());
        }
    }

    /// <summary>
    /// Fades out the arrow keys movement hint then destroys self.
    /// </summary>
    /// <returns>An IEnumerator for coroutine timing.</returns>
    private IEnumerator FadeArrowKeyHint()
    {
        float elapsed = 0f;

        Color[] originalColours = new Color[arrowKeys.Length];
        for (int i = 0; i < arrowKeys.Length; i++)
        {
            originalColours[i] = arrowKeys[i].color;
        }

        while (elapsed < fadeOutDuration)
        {
            float t = elapsed / fadeOutDuration;

            for (int i = 0; i < arrowKeys.Length; i++)
            {
                Color newColour = originalColours[i];
                newColour.a = Mathf.Lerp(1f, 0f, t);
                arrowKeys[i].color = newColour;
            }

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // enable gameplay
        gameManager.GetComponent<GameManager>().SetGameplay(true);
        Destroy(gameObject);
    }
}
