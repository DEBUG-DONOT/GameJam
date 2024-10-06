using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int attack;
    public int Speed;
    void Awake()
    {
        Speed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Player.GetInstance.gameObject.transform.position - transform.position).magnitude >= 80)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null)
        {
            if (collider.tag == ("Cell"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
