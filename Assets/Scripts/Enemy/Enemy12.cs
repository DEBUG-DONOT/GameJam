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
        timer = 1f;
    }
    private void FixedUpdate()
    {
        
        timer-=Time.deltaTime;
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
        transform.Rotate(Vector3.forward,60*Time.deltaTime,Space.World);
    }
    public void LaterUpdate()
    {
        if (Vector2.Distance(transform.position, player.transform.position) >= 70)
            Destroy(this.gameObject);
        Debug.Log("The distance is " + Vector2.Distance(transform.position, player.transform.position));
    }
    public void ChasePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
