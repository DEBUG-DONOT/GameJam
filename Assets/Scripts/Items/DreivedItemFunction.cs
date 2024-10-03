/*
 * 所有具体的功能在这里是实现
 */

//决定道具生效
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;

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







