using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : Enemy
{
    private void Awake()
    {
        HP = 30;
        mass = 5;
        attack = 12;
        rangedAttack = 10;
        timer = 2f;
        speed = 6f;
        organic = 90;
        SearchRange = 15;
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