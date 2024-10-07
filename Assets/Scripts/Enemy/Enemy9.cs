using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy9 : Enemy
{
    private float DashSpeed;
    // Start is called before the first frame update
    private void Awake()
    {
        HP = 50;
        mass = 7;
        attack = 70;
        rangedAttack = 15;
        speed = 6f;
        organic = 40;
        SearchRange = 15;
        DashSpeed = 20;
        timer = 0;
    }
    private void Update()
    {
        if (timer <= 0)
        {
            TryDash();
        }
        else
        {
            timer-=Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        if (transform.position.y >= -4&&timer<=10&&timer>0)
        {
            GetComponent<Rigidbody2D>().velocity= new Vector2(0, -speed);
        }
        else if (transform.position.y <= -4&&timer>0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
        }
    }
    private void TryDash()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <=3&&player.transform.position.y<transform.position.y+30)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,DashSpeed);
            timer = 14;
        }
    }
}
