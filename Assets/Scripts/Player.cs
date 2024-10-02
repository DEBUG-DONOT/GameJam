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
        Controller();
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
        //移动状态机
        float horizontalF = Input.GetAxis(AxisMacro.HorizontalString);
        if(horizontalF==0f) currStates=PlayerStates.Idle;
        else currStates = PlayerStates.Move;

        switch (currStates)
        {
            case PlayerStates.Idle:
                //do nothing
                break;
            case PlayerStates.Move:
                currStates = PlayerStates.Move;
                transform.Translate(new Vector3(horizontalF, 0f, 0f) * moveSpeed * Time.deltaTime);
                break;
        }
    }

    //应该使用单例模式
    //只有一个player
    private Player(){ }
    private static Player player=null;
    //移动状态机变量
    enum PlayerStates
    {
        Idle, Move, Jump
    }
    private PlayerStates currStates= PlayerStates.Idle;
}

