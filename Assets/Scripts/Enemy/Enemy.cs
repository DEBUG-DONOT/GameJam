using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Rigidbody2D rb;
    public int mass;
    public int attack;
    public int rangedAttack;
    public int organic;
    public float timer;
    private void Start()
    {
        player=Player.GetInstance.gameObject;
        rb=GetComponent<Rigidbody2D>();
    }
    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null) 
        {
            if (collider.tag == ("CellBullet"))
            {
                HP-=collider.GetComponent<CellBullet>().attack;
            }
        }
    }
    protected void Die()
    {
        SoundManager.GetInstance.Play("EnemyDie");
        Destroy(this.gameObject);
    }
    Vector3 randomVector;
    [SerializeField]
    protected float speed;

    public float SearchRange;
    [SerializeField]
    protected int hp;
    public int HP
    {  
        get 
        { 
            return hp; 
        } 
        set 
        { 
            hp = value; 
            if(hp <= 0)
            {
                Die();
            }
        }
    }
}
