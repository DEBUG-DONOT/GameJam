using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    private float timer;
    private void Awake()
    {
        HP = 3;
        mass = 1;
        attack = 0;
        timer = 1.0f;
    }
    private void Update()
    {
        timer-=Time.deltaTime;
        if (timer <= 0)
        {
            Vector3 min = new Vector3(-10, -10, 0);
            Vector3 max = new Vector3(10, 10, 0);
            Vector3 randomVector = new Vector3(Random.Range(min.x, max.x), Random.Range(min.x, max.x), Random.Range(min.x, max.x)).normalized;

        }
    }
}
