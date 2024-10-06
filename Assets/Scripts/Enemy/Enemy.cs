using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected GameObject player;
    public Rigidbody2D rb;
    public int mass;
    public int attack;
    public int rangedAttack;
    public int organic;
    public float timer;
    public void FixedUpdate()
    {
        if((Player.GetInstance.gameObject.transform.position-transform.position).magnitude>=100)
            Destroy(this.gameObject);
        if(Vector3.Distance(this.transform.position,player.transform.position)>SearchRange)
        {

            transform.Translate(randomVector*speed*Time.deltaTime);
        }
        else
        {
            ChasePlayer();
        }
    }
    public void ChasePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
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
