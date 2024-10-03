
//基础道具类型
public class SingleEffectItem:Item
{
    public override void Do_Item_Function()
    {
        base.Do_Item_Function();
        Destroy(gameObject);
    }
}

public class LongLastingItem : Item //永久
{
    public override void Do_Item_Function()
    {
        base.Do_Item_Function();
    }
}

public class RandomItem :Item
{

}

public class ChangeWithTimeItem :Item
{

}

////////////////////////////////////////////////////////
////具体道具类型
//////////////////////////////////////////////////////
public class Bullet:LongLastingItem
{
   Bullet()
    {
        this.Add_Item_Function(new SingleDirectionMove());
    }
}
