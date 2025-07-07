using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour
{
    [Header("Timings")]
    public float spawnDelayTime;
    public float activeTime;

    protected Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // spawn obstacle with random rotation
        float z = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, 0f, z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
