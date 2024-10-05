using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mouth : CellBase
{
    public GameObject AttachedMito;//按按钮时绑定
    public bool hasMito;
    private void Awake()
    {
        maxOrganic = 0;
        maxEnergy = 0;
        mass = 0;
        cost = 1;
        type = organelleType.Mouth;
        CellEnergy = 6;
        getEnergy = 0;
        loseEnergy = 0;
        needEnergy = 1;
        productEnergy = 0;
        needOrganic = 0;
        productOrganic = 0;
        hasMito = true;
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
        CellEnergy += getEnergy - loseEnergy;
        Organic = getOrganic;
        getEnergy = 0;
        loseEnergy = 0;
        getOrganic = 0;
        if (CellEnergy < -6)
        {
            Destroy(this.gameObject);
        }
        if (Organic > 0)
        {
            AttachedMito.GetComponent<CellBase>().GetOrganic(Organic);
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
            else if (collision.gameObject.tag == "Organic")
            {
                GetOrganic(collision.gameObject.GetComponent<Organic>().organic);
            }
        }
    }
}
