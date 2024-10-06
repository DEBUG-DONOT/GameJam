using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy12 : Enemy
{
    private void Awake()
    {
        HP =24 ;
        mass = 5;
        attack = 30;
        speed = 9f;
        organic = 8;
        SearchRange = 12;
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
