using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mouth : CellBase
{
    private void Awake()
    {
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
        Player.GetInstance.getEnergy-=needEnergy;
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Player.GetInstance.Energy--;
            }
            else if (collision.gameObject.tag == "Organic")
            {
                Player.GetInstance.AllOrganic++;
            }
        }
    }
}
