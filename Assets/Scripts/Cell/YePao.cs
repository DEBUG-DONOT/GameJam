using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YePao : CellBase
{
    // Start is called before the first frame update
    void Awake()
    {
        Player.GetInstance.maxEnergy+=20;
        cost = 3;
        type = organelleType.YePao;
        needEnergy = 1;
        timer = 1.0f;
    }

    // Update is called once per frame
    void Update()
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
        Player.GetInstance.Energy -= needEnergy;
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
