using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour
{
    [Header("Timings")]
    [SerializeField] private float startAnimDuration;
    [SerializeField] private float obstacleActiveDuration;
    [SerializeField] private float destroyAnimDuration;

    // Start is called before the first frame update
    void Start()
    {
        // let warning anim play out
    }

    // Update is called once per frame
    void Update()
    {

    }

    // let obstacle do its thing while active
    // destroy after active duration
}
