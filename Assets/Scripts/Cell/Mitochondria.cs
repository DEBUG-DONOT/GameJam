using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CellBase;

public class NewBehaviourScript : CellBase
{
    // Start is called before the first frame update
    private void Awake()
    {

        Player.GetInstance.mass += 1;
        cost = 3;
        type = organelleType.Mitochondria;
        productEnergy = 12;
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
        if (Player.GetInstance.AllOrganic >=needOrganic)
        {
            Player.GetInstance.AllOrganic--;
            Player.GetInstance.Energy += productEnergy;
        }
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
