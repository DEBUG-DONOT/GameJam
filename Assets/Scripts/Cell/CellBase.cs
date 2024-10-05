using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBase : MonoBehaviour
{

    public enum organelleType
    {
        Blank, Chloroplast, Mitochondria, Mouth, Flagellum, CellSpine, Pipe, Capsid
    }
    public organelleType type;
    public int cost;
    public int mass;
    public int driveForce;
    public float timer;
    public float radius;
    #region 胞内能量
    [SerializeField] protected int cellEnergy;
    public int CellEnergy
    {
        set { cellEnergy = value; }
        get { return cellEnergy; }
    }
    protected int getEnergy;
    protected int loseEnergy;
    public void GetEnergy(int energy)
    {
        getEnergy += energy;
    }
    public void LoseEnergy(int energy)
    {
        loseEnergy += energy;
    }
    protected int productEnergy;
    public int needEnergy;
    public int maxEnergy;
    #endregion
    #region 有机物
    [SerializeField] protected int organic;
    public int Organic
    {
        set
        {
            organic = value;
            if (organic < 0)
            {
                Organic = 0;
            }
        }
        get { return organic; }
    }
    protected int getOrganic;
    protected int loseOrganic;
    public void GetOrganic(int organic)
    {
        getOrganic += organic;
    }
    public void LoseOrganic(int organic)
    {
        loseOrganic += organic;
    }
    public int needOrganic;
    public int productOrganic;
    protected int maxOrganic;
    #endregion
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }
}