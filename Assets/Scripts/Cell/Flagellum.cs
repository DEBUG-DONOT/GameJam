using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flagellum : CellBase
{
    // Start is called before the first frame update
    void Awake()
    {
        maxOrganic = 0;
        maxEnergy = 0;
        mass = 0;
        cost = 3;
        type = organelleType.Flagellum;
        CellEnergy = 6;
        getEnergy = 0;
        loseEnergy = 0;
        needEnergy = 6;
        driveForce = 3;
    }

    // Update is called once per frame
    void Update()
    {
        CellEnergy += getEnergy - loseEnergy;
        getEnergy = 0;
        loseEnergy = 0;
        if (CellEnergy < -6)
        {
            Destroy(this.gameObject);
        }
        if (CellEnergy > maxEnergy) CellEnergy = maxEnergy;
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
