using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    private void Awake()
    {
        HP = 15;
        mass = 1;
        speed = 5;
        attack = 0;
        timer = 2f;
        organic =10;
    }
    private void Update()
    {
        timer-=Time.deltaTime;
        if (timer <= 0)
        {
            int min = -10;
            int max = 10;
            Vector3 randomVector = new Vector3(Random.Range(min, max), Random.Range(min, max), 0).normalized;
            rb.velocity = randomVector*speed;
            timer=2f;
        }
    }

}
