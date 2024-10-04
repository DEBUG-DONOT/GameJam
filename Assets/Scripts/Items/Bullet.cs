
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : LongLastingItem
{
    Bullet()
    {
        //this.Add_Item_Function(new SingleDirectionMove());
        this.Add_Item_Function(new ChaseMouseMove());
    }
    private void Update()
    {
        Do_Item_Function();
    }
    [SerializeField]
    private float BulletSpeed = 1;

}
