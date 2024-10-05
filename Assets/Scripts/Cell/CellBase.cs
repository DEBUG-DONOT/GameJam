using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBase : MonoBehaviour
{
    public enum organelleType
    {
        Blank,Chloroplast,Mitochondria,Mouth,Flagellum,CellSpine, Pipe,Capsid
    }
    public organelleType type;
    #region °ûÄÚÄÜÁ¿
    [SerializeField]private int cellEnergy;
    public int CellEnergy
    {
        set { cellEnergy = value; }
        get { return cellEnergy; }
    }
    private int getEnergy;
    private int loseEnergy;
    public void GetEnergy(int energy)
    {
        getEnergy += energy;
    }
    public void LoseEnergy(int energy)
    {
        loseEnergy-=energy;
    }
    #endregion
}