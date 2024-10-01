using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        float horizontalF = Input.GetAxis(AxisMacro.HorizontalString);
        float verticalF=Input.GetAxis(AxisMacro.VerticalString);
        transform.Translate(new Vector3(horizontalF, verticalF,0)*moveSpeed*Time.deltaTime);
    }
    public static Player GetInstance()
    {
        if (player == null)
        {
            player = new Player();
        }
        return player;
    }
    protected override void Controller()
    {
        //实现玩家的操作
        //注意和动画的联动-状态机
        //base.Controller();
        

    }

    //应该使用单例模式
    //只有一个player
    private Player(){ }
    private static Player player=null;


}
