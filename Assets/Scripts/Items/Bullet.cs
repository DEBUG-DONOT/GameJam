
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : LongLastingItem
{
    Bullet()
    {
        this.Add_Item_Function(new SingleDirectionMove());
    }
    private void Update()
    {
        Do_Item_Function();
    }

}
