using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YePao : CellBase
{
    // Start is called before the first frame update
    void Awake()
    {
        Player.GetInstance.maxEnergy+=20;
        Player.GetInstance.maxOrganic += 3;
        cost = 3;
        type = organelleType.YePao;
        needEnergy = 1;
        timer = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            TryUpdate();
            timer = 0.3f;
        }
    }
    private void TryUpdate()
    {
        Player.GetInstance.getEnergy -= needEnergy;
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Player.GetInstance.Energy--;
                Player.GetInstance.rb.AddForce((transform.position - collision.gameObject.transform.position).normalized * 100);
            }
        }
    }
}
