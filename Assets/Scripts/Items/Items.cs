using System.Collections.Generic;
using UnityEngine;


/*
 * Item作为MOnoBehaviour挂在到具体的道具上
 */

public class Item:MonoBehaviour
{

    public virtual void Do_Item_Function() 
    {
            foreach (ItemFunction curr in itemFunctions) curr.Do_function();
        
    }

    private List<ItemFunction> itemFunctions = new List<ItemFunction>();
}

/*
 * ItemFunction作为父类，所有实现具体功能的道具继承这个类
 */

public class ItemFunction
{
    public virtual void Do_function() { }//这是一个实现具体功能的函数，加血扣血什么的在这里实现，直接掉player接口，记得调用单例给的
}
