using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        Vector3 min = new Vector3(-10, -10, 0);
        Vector3 max = new Vector3(10, 10, 0);
        randomVector = new Vector3(Random.Range(min.x, max.x), Random.Range(min.x, max.x), Random.Range(min.x, max.x));
        randomVector.Normalize();
        player = Player.GetInstance.gameObject;
        HP = 3;
    }
    private void FixedUpdate()
    {
        if(Vector3.Distance(this.transform.position,player.transform.position)>SearchRange)
        {

            transform.Translate(randomVector*speed*Time.deltaTime);
        }
        else
        {
            ChasePlayer();
        }
    }
    void ChasePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null) 
        {
            if (collider.tag == ("CellBullet"))
            {
                HP-=collider.GetComponent<CellBullet>().attack;
            }
        }
    }
    Vector3 randomVector;
    [SerializeField]
    private float speed=1.0f;
    [SerializeField]
    private float SearchRange = 5.0f;
    [SerializeField]
    private int hp;
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
                Destroy(this.gameObject);
            }
        }
    }
}
