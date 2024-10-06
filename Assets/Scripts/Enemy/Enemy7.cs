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
        GetComponent<Rigidbody>().velocity = randomVector * speed;
    }
    private void FixedUpdate()
    {

        if (Vector3.Distance(this.transform.position, player.transform.position) >SkipRange)
        {
            int min = -10;
            int max = 10;
            Vector3 randomVector = new Vector3(Random.Range(min, max), Random.Range(min, max), 0).normalized;
            transform.Translate(randomVector * speed * Time.deltaTime);
        }
        else
        {
            RunAway();
        }
    }
    private void RunAway()
    {
        Vector2 dir = transform.position - player.transform.position;
        transform.Translate(dir * Time.deltaTime*speed);
    }
}
