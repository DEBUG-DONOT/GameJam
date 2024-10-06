using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    private void Awake()
    {
        HP = 10;
        mass = 2;
        attack = 5;
        speed = 5.0f;
        organic =10;
        SearchRange = 4;
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
