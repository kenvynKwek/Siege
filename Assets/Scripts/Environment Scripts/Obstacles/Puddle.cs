using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : ObstacleBase
{
    private float puddleSlowedSpeed = 0.7f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().SlowSpeed(puddleSlowedSpeed);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().OriginalSpeed();
        }
    }
}
