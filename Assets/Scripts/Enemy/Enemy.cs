using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected GameObject player;
    public int mass;
    public int attack;
    public int organic;
    protected void Start()
    {
        Vector3 min = new Vector3(-10, -10, 0);
        Vector3 max = new Vector3(10, 10, 0);
        randomVector = new Vector3(Random.Range(min.x, max.x), Random.Range(min.x, max.x), Random.Range(min.x, max.x));
        randomVector.Normalize();
        player=Player.GetInstance.gameObject;

    }
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
    protected float speed=1.0f;
    [SerializeField]
    protected float SearchRange = 5.0f;
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
