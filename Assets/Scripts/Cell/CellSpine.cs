using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CellBase;

public class CellSpine : CellBase
{
    public Vector2 dir;
    public GameObject BulletPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        needEnergy=8;
        timer = 5f;
        dir=transform.position-Player.GetInstance.gameObject.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            TryFire();
            timer = 5f;
        }
    }
    private void TryFire()
    {
        Player.GetInstance.Energy-=needEnergy;
        GameObject.Instantiate(BulletPrefab,transform.position,transform.rotation);
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Player.GetInstance.Energy--;
            }
        }
    }
}
