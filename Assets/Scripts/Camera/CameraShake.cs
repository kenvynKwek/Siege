using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    void Awake()
    {
        Instance = this; // singleton pattern
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Shakes the camera with a specified duration and intensity.
    /// </summary>
    /// <param name="duration">How long the shake lasts for.</param>
    /// <param name="magnitude">How intense the shake is.</param>
    /// <returns>An IEnumerator for coroutine timing.</returns>
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
    /// Triggers a camera shake effect with a specified duration and intensity.
    /// </summary>
    /// <param name="duration">How long the shake lasts for.</param>
    /// <param name="magnitude">How intense the shake is.</param>
    public void ShakeCamera(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }
}
