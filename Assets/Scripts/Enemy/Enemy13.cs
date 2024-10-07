using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy13 : Enemy
{
    private void Awake()
    {
        HP = 70;
        mass = 5;
        attack = 40;
        speed = 20f;
        organic = 60;
        SearchRange = 18;
    }
    private bool isDash=false;

    // Update is called once per frame
    void Update()
    {
        
    }
}
