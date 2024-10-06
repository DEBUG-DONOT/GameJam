using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBullet : MonoBehaviour
{
    public int attack;
    public int Speed;
    private void Awake()
    {
        attack = 1;
        Speed = 20;
        GetComponent<Rigidbody2D>().velocity = (transform.position-Player.GetInstance.gameObject.transform.position).normalized*Speed;
    }
    private void Update()
    {
        if ((Player.GetInstance.gameObject.transform.position - transform.position).magnitude >= 80)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.collider.GetComponent<Enemy>().HP-=attack;
            }
        }
    }
}
