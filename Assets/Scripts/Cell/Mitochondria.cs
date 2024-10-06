using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CellBase;

public class NewBehaviourScript : CellBase
{
    public GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("player");
        player.GetComponent<Player>().mass += 1;
        cost = 3;
        type = organelleType.Mitochondria;
        productEnergy = 9;
        needOrganic = 1;
        timer = 1.0f;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            TryUpdate();
            timer = 1.0f;
        }
    }
    // Update is called once per frame
    private void TryUpdate()
    {
        if (player.GetComponent<Player>().AllOrganic >=needOrganic)
        {
            player.GetComponent<Player>().AllOrganic--;
            player.GetComponent<Player>().Energy += productEnergy;
        }
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
