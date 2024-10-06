using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Enemy
{
    private void Awake()
    {
        SearchRange = 15;
        HP = 10;
        rangedAttack = 8;
        mass = 1;
        organic = 10;
        timer = 2;
        speed = 3;
        int min = -3;
        int max = 3;
        Vector3 randomVector = new Vector3(Random.Range(min, max), 0, 0).normalized;
        GetComponent<Rigidbody>().velocity = randomVector*speed;
    }
}
