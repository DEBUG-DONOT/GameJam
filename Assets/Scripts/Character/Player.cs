using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        float verticalF = Input.GetAxis(AxisMacro.VerticalString);
        //根据状态移动角色
        switch (currStates)
        {
            case PlayerStates.Idle:
                //do nothing
                if (horizontalF != 0) currStates = PlayerStates.Move;
                break;
            case PlayerStates.Move:
                currStates = PlayerStates.Move;
                transform.Translate(new Vector3(horizontalF, 0f, 0f) * moveSpeed * Time.deltaTime);
                Debug.Log("player state is " + currStates);
                break;
            case PlayerStates.Jump:
                transform.Translate(new Vector3(0f, verticalF, 0f) * moveSpeed * Time.deltaTime * 1000);
                //rb.AddForce(new Vector2(0, 10));
                Debug.Log("player state is " + currStates);
                break;
        }
    }

    //应该使用单例模式
    //只有一个player
    private Player() { }
    private static Player player = null;
    private Rigidbody2D rb = null;
    //移动状态机变量
    enum PlayerStates
    {
        Idle, Move, Jump
    }

    private PlayerStates currStates = PlayerStates.Idle;
    private KeyCode JumpKeyCode = KeyCode.W;

    #region MP
    [SerializeField] private int mp;
    [SerializeField] private int maxMP;
    public int MP
    {
        set
        {
            mp = value;
            if (mp > maxMP)
            {
                MP = maxMP;
            }
            if (mp < 0)
            {
                MP = 0;
            }
        }
        get
        {
            return mp;
        }
    }
    #endregion

    #region Jump
    [SerializeField] private float jumpHight;
    [SerializeField] private float minJumpHight;
    [SerializeField] private int maxJumpTimes = 1;
    public float JumpHight
    {
        set
        {
            jumpHight = value;
            if (jumpHight < minJumpHight) JumpHight = minJumpHight;
        }
        get
        {
            return jumpHight;
        }
    }
    public int MaxJumpTimes
    {
        set
        {
            maxJumpTimes = value;
        }
        get
        {
            return maxJumpTimes;
        }
    }
    #endregion

    #region Luck
    [SerializeField] private int luck;
    public int Luck
    {
        set
        {
            luck = value;
        }
        get
        {
            return luck;
        }
    }
    #endregion

    #region Durity
    [SerializeField] private int durity;
    [SerializeField] private int maxDurity;
    public int Durity
    {
        set
        {
            durity = value;
            if (durity > maxDurity) Durity = maxDurity;
            if (durity < 0) Durity = 0;
        }
        get
        {
            return durity;
        }
    }
    #endregion

    #region Dash
    [SerializeField] private float dashDistance;
    public float DashDistance
    {
        set
        {
            dashDistance = value;
            if (dashDistance < 0) DashDistance = 0;
        }
        get
        {
            return dashDistance;
        }
    }
    #endregion

    #region Hate and Peace
    [SerializeField] private int hate;
    [SerializeField] private float peaceTime;
    public int Hate
    {
        set
        {
            hate = value;
        }
        get
        {
            return hate;
        }
    }
    public float PeaceTime
    {
        set
        {
            peaceTime = value;
        }
        get
        {
            return peaceTime;
        }
    }
    #endregion

    #region Hungry and Sanity
    [SerializeField] private int hungry;
    [SerializeField] private int sanity;
    public int Hungry
    {
        set
        {
            hungry = value;
        }
        get
        {
            return hungry;
        }
    }
    public int Sanity
    {
        set
        {
            sanity = value;
        }
        get
        {
            return sanity;
        }
    }
    #endregion
}

