using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chloroplast : CellBase
{
    public GameObject player;
    private void Awake()
    {

        player = GameObject.Find("player");
        player.GetComponent<Player>().mass += 1;
        cost = 3;
        player = GameObject.Find("player");
        type = organelleType.Chloroplast;
        productEnergy = 18;
        productOrganic = 4;
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
    private void TryUpdate()
    {
        productEnergy = 18-(int)(0.5*transform.position.y);
        productOrganic = 4 - (int)(0.3 * transform.position.y);
        if(productOrganic < 1) productOrganic = 1;
        if(productEnergy < 2) productEnergy = 2;
        player.GetComponent<Player>().Energy += productEnergy;
        player.GetComponent<Player>().AllOrganic += productOrganic;
    }
public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                player.GetComponent<Player>().Energy--;
            }
        }
    }
}
