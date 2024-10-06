using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public static Player GetInstance;
    private void Awake()
    {
        GetInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("player no rigidbody2D!");
        canMove = true;
        maxEnergy = 90;
        Energy = maxEnergy;
        AllOrganic = 0;
        ShowEnergy=Energy;
        mass = 2;
}

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Energy -= 4;
            timer = 1.0f;
            ShowEnergy = Energy;
            if (Energy < 0) UIManager.GetInstance.EnterPanel(EndScene.GetInstance.gameObject);

        }
    }
    private void FixedUpdate()
    {
        Controller();
        if(rb.angularVelocity>maxAngle) rb.angularVelocity = maxAngle;
        if(rb.angularVelocity<-maxAngle) rb.angularVelocity =- maxAngle;
    }
    /*public static Player GetInstance()
    {
        if (player == null)
        {
            player = new Player();
        }
        return player;
    }*/
    protected override void Controller()
    {   
        if (canMove)
        {
            playerMoveDirection = new Vector2(Input.GetAxis("Horizontal") * MoveSpeed/mass * Time.deltaTime, Input.GetAxis("Vertical") * MoveSpeed/mass * Time.deltaTime);
            transform.position+=new Vector3(playerMoveDirection.x,playerMoveDirection.y,0f);
            
            if (Input.GetKey(KeyCode.Q))
            {
                // rotationNumber += 3;
                rb.AddTorque(1);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                // rotationNumber -= 3;
                rb.AddTorque(-1);
            }
            //Quaternion rotation = Quaternion.Euler(0,0,transform.rotation.z+rotationNumber);
           // transform.rotation = rotation;
        }
    }


    /*protected override void Controller()
    {
        //移动状态机
        float horizontalF = MoveSpeed*Time.deltaTime;
        float verticalF = jumpHight*Time.deltaTime;
        //根据状态移动角色
        switch (currStates)
        {
            case PlayerStates.Idle:
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    currStates = PlayerStates.Move;
                    if (Input.GetKey(KeyCode.A)) playerMoveDirection = new Vector3(-horizontalF, 0, 0);
                    else playerMoveDirection = new Vector3(horizontalF, 0, 0);
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    currStates = PlayerStates.JumpUP;
                    playerMoveDirection = new Vector3(0, verticalF, 0);
                }
                break;
            case PlayerStates.Move:
                //先移动
                //rb.AddForce(playerMoveDirection,ForceMode2D.Impulse);
                transform.Translate(playerMoveDirection);
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) ;
                else if(Input.GetKey(KeyCode.W))
                {
                    currStates = PlayerStates.JumpUP;
                    playerMoveDirection = new Vector3(0, verticalF, 0);
                }
                else currStates = PlayerStates.Idle;
                Debug.Log("player state is " + currStates);
                break;

                //如果是俯视角的游戏就不需要jump
            //case PlayerStates.JumpUP:
            //    //rb.AddForce(playerMoveDirection, ForceMode2D.Impulse);
            //   // transform.Translate(playerMoveDirection); 
            //    transform.position = transform.position+new Vector3(0,jumpHight,0);
            //    Debug.Log("player state is " + currStates);
            //    currStates = PlayerStates.JumpDOWN;
            //    break;

            //case PlayerStates.JumpDOWN:
            //    //if() //如果碰到地面了
            //    break;
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        //如果撞到tag为ground的物体就让onground为false
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision == null) return;
        //如果离开tag为ground的物体就让onground为true，但是也不一定这么做
    }

    //应该使用单例模式
    //只有一个player
    /*private Player() { }
    private static Player player = null;*/
    public Rigidbody2D rb = null;
    //移动状态机变量
    enum PlayerStates
    {
        Idle, Move, JumpUP, JumpDOWN
    }
    private bool onGround = true;//只有onGround==true才能起跳
    [SerializeField]
    float drag = 1;//力衰减的速度，调节手感
    Vector2 playerMoveDirection = Vector2.zero;
    private PlayerStates currStates = PlayerStates.Idle;
    private KeyCode JumpKeyCode = KeyCode.W;
    public bool canMove ;
    [SerializeField] private float rotationNumber=0;
    static float timer = 0;

    [SerializeField]
    private float maxAngle = 2;

    #region 整体参数
    public int maxEnergy;
    [SerializeField] private int energy;
    public int Energy
    {
        set 
        {
            energy = value;
            if(energy > maxEnergy) {Energy = maxEnergy;}
        }
        get { return energy; }
    }
    public int ShowEnergy;
    public int AllOrganic;
    public int mass;
    #endregion

    #region Jump
    [SerializeField] private float jumpHight=2;
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

}

