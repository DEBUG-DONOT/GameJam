using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBase : MonoBehaviour
{
    public enum organelleType
    {
        Chloroplast, Mitochondria, Mouth, Flagellum, CellSpine,Capsid,YePao,Shell
    }
    public organelleType type;
    public int cost;
    public float timer;

    public int productEnergy;
    public int needEnergy;
    public int needOrganic;
    public int productOrganic;
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null)
        {
            if (collider.tag == ("EnemyBullet"))
            {
                Player.GetInstance.Energy -= collider.GetComponent<EnemyBullet>().attack;
            }
        }
    }
}