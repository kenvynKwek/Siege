using System.Collections;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private Camera cam;
    private Coroutine zoomInCoroutine;

    public static CameraEffects Instance;
    public Transform player;

    void Awake()
    {
        Instance = this; // singleton pattern
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // camera follow player
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position += new Vector3(x, y, 0f); // shake screen

            elapsedTime += Time.deltaTime; // update elapsed time
            yield return null;
        }
    }


    /// <summary>
    /// Shakes the camera with a specified duration and intensity.
    /// </summary>
    /// <param name="duration">How long the shake lasts for.</param>
    /// <param name="magnitude">How intense the shake is.</param>
    public void ShakeCamera(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator SmoothZoom(float zoomCamSize, float duration)
    {
        float curCamSize = cam.orthographicSize;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            cam.orthographicSize = Mathf.Lerp(curCamSize, zoomCamSize, t);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        cam.orthographicSize = zoomCamSize;
    }

    /// <summary>
    /// Simulates a camera zoom in effect.
    /// </summary>
    /// <param name="zoomCamSize">The final camera size to zoom into.</param>
    /// <param name="duration">The duration of the zoom.</param>
    /// <returns></returns>
    public void ZoomIn(float zoomCamSize, float duration)
    {
        if (zoomInCoroutine != null)
        {
            StopCoroutine(zoomInCoroutine);
        }

        zoomInCoroutine = StartCoroutine(SmoothZoom(zoomCamSize, duration));
    }
}
