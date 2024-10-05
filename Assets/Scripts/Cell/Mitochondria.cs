using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CellBase;

public class NewBehaviourScript : CellBase
{
    // Start is called before the first frame update
    private void Awake()
    {
        maxOrganic = 4;
        maxEnergy = 1;
        mass = 1;
        cost = 3;
        type = organelleType.Mitochondria;
        CellEnergy = 6;
        getEnergy = 0;
        loseEnergy = 0;
        needEnergy = 3;
        productEnergy = 21;
        needOrganic = 5;
        productOrganic = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CellEnergy += getEnergy - loseEnergy;
        Organic += getOrganic-loseOrganic;
        getEnergy = 0;
        loseEnergy = 0;
        getOrganic = 0;
        loseOrganic = 0;
        if (CellEnergy < -6)
        {
            Destroy(this.gameObject);
        }
        if (CellEnergy > 0)
        {

        }
        if (Organic >= needOrganic)
        {
            LoseOrganic(needOrganic);
            GetEnergy(productEnergy);
        }
        if (CellEnergy > maxEnergy) CellEnergy = maxEnergy;
        if (Organic > maxOrganic) Organic = maxOrganic;
        LoseEnergy(needEnergy);
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
