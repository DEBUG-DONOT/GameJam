using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : Enemy
{
    private void Awake()
    {
        HP = 20;
        mass = 3;
        attack = 9;
        speed = 5.0f;
        organic = 4;
        SearchRange = 9;
    }
    private void FixedUpdate()
    {

        timer -= Time.deltaTime;
        if (Vector3.Distance(this.transform.position, player.transform.position) > SearchRange)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                int min = -10;
                int max = 10;
                Vector3 randomVector = new Vector3(Random.Range(min, max), Random.Range(min, max), 0).normalized;
                rb.velocity = randomVector * speed;
                timer = 1f;
            }
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
