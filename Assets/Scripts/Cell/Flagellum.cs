using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flagellum : CellBase
{
    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("player");
        player.GetComponent<Player>().MoveSpeed++;
        cost = 3;
        type = organelleType.Flagellum;
        needEnergy = 6;
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
        player.GetComponent<Player>().Energy -= needEnergy;
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
