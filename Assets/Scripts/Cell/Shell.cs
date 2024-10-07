using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CellBase;

public class Shell : CellBase
{
    [SerializeField]private int hp;
    public int HP
    {
        set
        {
            hp = value;
            if (hp <= 0) Destroy(this.gameObject);
        }
        get
        {
            return hp;
        }
    }
    void Awake()
    {
        cost = 3;
        type = organelleType.Shell;
        needEnergy = 1;
        timer = 0.3f;
        HP = 3;
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
                HP--;
                Player.GetInstance.rb.AddForce((transform.position - collision.gameObject.transform.position).normalized * BoundForce);
            }
        }
    }
}
