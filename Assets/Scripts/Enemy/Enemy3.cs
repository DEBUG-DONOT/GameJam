using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{
    private void Awake()
    {
        HP = 12;
        mass = 1;
        attack = 10;
        timer = 1.0f;
        speed = 5.0f;
        organic = 8;
        SearchRange = 5;
    }
    private void FixedUpdate()
    {

        if (Vector3.Distance(this.transform.position, player.transform.position) > SearchRange)
        {
            int min = -10;
            int max = 10;
            Vector3 randomVector = new Vector3(Random.Range(min, max), Random.Range(min, max), 0).normalized;
            transform.Translate(randomVector * speed * Time.deltaTime);
        }
        else
        {
            ChasePlayer();
        }
    }
    public void ChasePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
