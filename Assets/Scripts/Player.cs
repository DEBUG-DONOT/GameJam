using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("player no rigidbody2D!");
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
        float verticalF=Input.GetAxis(AxisMacro.VerticalString);
        //根据状态移动角色
        switch (currStates)
        {
            case PlayerStates.Idle:
                //do nothing
                break;
            case PlayerStates.Move:
                currStates = PlayerStates.Move;
                transform.Translate(new Vector3(horizontalF, 0f, 0f) * moveSpeed * Time.deltaTime);
                Debug.Log("player state is " + currStates);
                break;
            case PlayerStates.Jump:
                transform.Translate(new Vector3(0f,verticalF , 0f) * moveSpeed * Time.deltaTime*1000);
                //rb.AddForce(new Vector2(0, 10));
                Debug.Log("player state is " + currStates);
                break;
        }
    }

    //应该使用单例模式
    //只有一个player
    private Player(){ }
    private static Player player=null;
    private Rigidbody2D rb = null;
    //移动状态机变量
    enum PlayerStates
    {
        Idle, Move, Jump
    }
    [SerializeField]
    private int MaxJumpTimes = 1;
    private PlayerStates currStates= PlayerStates.Idle;
    private KeyCode JumpKeyCode = KeyCode.W;
}

