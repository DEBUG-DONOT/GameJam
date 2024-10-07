using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7 : Enemy
{
    private int SkipRange;
    private void Awake()
    {
        SearchRange = 12;
        SkipRange = 6;
        HP = 15;
        rangedAttack = 9;
        mass = 2;
        organic = 4;
        timer = 2;
        speed = 3;
        int min = -3;
        int max = 3;
        Vector3 randomVector = new Vector3(Random.Range(min, max), 0, 0).normalized;
        GetComponent<Rigidbody2D>().velocity = randomVector * speed;
    }
    private void FixedUpdate()
    {
        timer-=Time.deltaTime;

        if (Vector3.Distance(this.transform.position, player.transform.position) >SkipRange)
        {
            if (timer < 0)
            {
                int min = -10;
                int max = 10;
                Vector3 randomVector = new Vector3(Random.Range(min, max), Random.Range(min, max), 0).normalized;
                rb.velocity=randomVector * speed;
                timer = 2;
            }
        }
        else
        {
            RunAway();
        }
    }
    private void RunAway()
    {
        Vector2 dir = transform.position - player.transform.position;
        rb.velocity=dir*speed;
    }
}
