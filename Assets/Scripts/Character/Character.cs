using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator != null) Debug.LogError(gameObject.name + "no animator!");
        _transform = transform;
        if (_transform != null) Debug.LogError(gameObject + "no transfomation!");
    }

    // Update is called once per frame
    void Update()
    {

    }
    //注意动作和动画的联动
    //enimy、npc都可以从character派生，在character中可以完成一部分的工作
    protected virtual void Controller()
    {
        // do nothing;
        //control 根据不同具体角色去更改

    }

    Transform _transform = null;
    Animator _animator = null;

    #region HP
    [SerializeField] protected int hp;//角色hp
    [SerializeField] protected int maxHP;
    public int HP
    {
        set
        {
            hp = value;
            if (hp > maxHP) HP = maxHP;
            if (hp == 0)
            {
                //死亡
            }
            if (hp < 0) HP = 0;
        }
        get
        {
            return hp;
        }
    }
    #endregion

    #region Money
    [SerializeField] protected int money;//怪物设为掉落金钱量，角色为携带金钱量
    public int Money
    {
        set
        {
            money = value;
            if (money < 0) Money = 0;
        }
        get
        {
            return money;
        }
    }
    #endregion

    #region MoveSpeed
    [SerializeField] protected float moveSpeed = 10.0f;
    public float MoveSpeed
    {
        set
        {
            moveSpeed = value;
            if (moveSpeed < 0) MoveSpeed = 0;
        }
        get
        {
            return moveSpeed;
        }
    }
    #endregion

    #region ImmuneTime
    [SerializeField] protected float immuneTime;
    public float ImmuneTime
    {
        set
        {
            immuneTime = value;
            if (immuneTime < 0) ImmuneTime = 0;
        }
        get
        {
            return immuneTime;
        }
    }
    #endregion

    //public Bullet[] bulletList; 或者换成vector

    #region Attack and Defence
    [SerializeField] protected int defence;
    [SerializeField] protected int attack;
    public int Defence
    {
        set
        {
            defence = value;
        }
        get
        {
            return defence;
        }
    }
    public int Attack
    {
        set
        {
            attack = value;
        }
        get
        {
            return attack;
        }
    }
    #endregion

    #region Durity(shield)
    [SerializeField] protected int durity;
    public int Durity
    {
        set
        {
            durity = value;
            if (durity < 0) Durity = 0;
        }
        get
        {
            return durity;
        }
    }
    #endregion

    #region Catagories
    public enum Catagories
    {
        Player
    }
    [SerializeField] protected Catagories catagory;
    //调用时检查是否匹配
    public Catagories Catagory
    {
        set
        {
            catagory = value;
        }
        get
        {
            return catagory;
        }
    }
    #endregion
}
