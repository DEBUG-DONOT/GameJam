using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy11 : Enemy
{
    // Start is called before the first frame update
    private void Awake()
    {
        HP = 25;
        mass = 3;
        attack = 25;
        speed = 10f;
        organic = 8;
        SearchRange = 15;
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
