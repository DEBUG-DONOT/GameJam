/*
 * 所有具体的功能在这里是实现
 */

//决定道具生效
using System.Collections.Generic;
using System.Numerics;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
/// <summary>
/// //////////////////////////////////////////////////////
/// 下面是具体生效的道具function
/// //////////////////////////////////////////////////////
/// </summary>
using EffectItemFunction = ItemFunction;

public class ChangeHP: EffectItemFunction
{
    //扣血
    public override void Do_function(Item item)
    {
        
    }
}

public class ChangeSpeed: EffectItemFunction
{
    //改移速
    public override void Do_function(Item item)
    {
        
    }
}

public class ChangeJumpHight : EffectItemFunction
{
    //跳跃高度
    public override void Do_function(Item item)
    {

    }
}

public class SingleDirectionMove : EffectItemFunction
{
    public override void Do_function(Item item)
    {
        item.gameObject.transform.Translate(new UnityEngine.Vector3(0.5f, 0, 0));
    }
}

public class ChaseMouseMove : EffectItemFunction
{
    public override void Do_function(Item item)
    {
        if(isFirst)
        {
            direction= Input.mousePosition- item.transform.position;
            direction=direction.normalized;
            isFirst =false;
        }
        item.transform.Translate(direction*ChangeSpeed*Time.deltaTime);
    }
    public float ChangeSpeed=1;
    UnityEngine.Vector3 direction;
    bool isFirst=true;
}








