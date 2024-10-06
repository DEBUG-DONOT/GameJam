using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Chloroplast : CellBase
{
    
    private void Awake()
    {


        Player.GetInstance.mass += 1;
        cost = 3;
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
        productEnergy = 8+(int)(transform.position.y);
        productOrganic = 4 +(int)(0.3 * transform.position.y);
        if(productOrganic < 1) productOrganic = 1;
        if(productEnergy < 2) productEnergy = 2;
        Player.GetInstance.getEnergy += productEnergy;
        Player.GetInstance.getOrganic += productOrganic;
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
