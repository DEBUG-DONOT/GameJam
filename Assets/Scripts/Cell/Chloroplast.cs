using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chloroplast : CellBase
{
    public GameObject player;
    public GameObject AttachedMouth;
    private void Awake()
    {
        mass = 1;
        cost = 3;
        player = GameObject.Find("player");
        type = organelleType.Chloroplast;
        maxEnergy = 2;
        maxOrganic = 1;
        CellEnergy = 6;
        getEnergy = 0;
        loseEnergy = 0;
        needEnergy = 0;
        productEnergy = 12;
        productOrganic = 4;
    }
    // Update is called once per frame
    void Update()
    {
        CellEnergy +=getEnergy-loseEnergy;
        Organic += getOrganic;
        getEnergy = 0;
        loseEnergy = 0;
        getOrganic = 0;
        if (CellEnergy < -6)
        {
            if(AttachedMouth != null)
            {
                Destroy(AttachedMouth);
            }
            Destroy(this.gameObject);
        }
        if (CellEnergy > 0)
        {
            
        }
        if(Organic > 0)
        { 
             
        }
        if(CellEnergy>maxEnergy)CellEnergy = maxEnergy;
        if(Organic>maxOrganic)Organic = maxOrganic;
        GetEnergy(productEnergy);
        LoseEnergy(needEnergy);
        GetOrganic(productOrganic);
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                CellEnergy--;
            }
        }
    }
}
