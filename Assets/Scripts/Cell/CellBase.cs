using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBase : MonoBehaviour
{
    public enum organelleType
    {
        Blank, Chloroplast, Mitochondria, Mouth, Flagellum, CellSpine, Pipe, Capsid,YePao
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
}