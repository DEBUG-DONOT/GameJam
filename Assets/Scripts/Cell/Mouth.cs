using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mouth : CellBase
{
    public GameObject player;
    private void Awake()
    {
        player = GameObject.Find("player");
        cost = 1;
        type = organelleType.Mouth;
        needEnergy = 1;
        timer = 1.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            TryUpdate();
            timer = 1.0f;
        }
    }
    void TryUpdate()
    {
        player.GetComponent<Player>().Energy-=needEnergy;
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                player.GetComponent<Player>().Energy--;
            }
            else if (collision.gameObject.tag == "Organic")
            {
                player.GetComponent<Player>().AllOrganic++;
            }
        }
    }
}
