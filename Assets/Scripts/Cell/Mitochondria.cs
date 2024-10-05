using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CellBase;

public class NewBehaviourScript : CellBase
{
    // Start is called before the first frame update
    private void Awake()
    {
        maxOrganic = 4;
        maxEnergy = 1;
        mass = 1;
        cost = 3;
        type = organelleType.Mitochondria;
        CellEnergy = 6;
        getEnergy = 0;
        loseEnergy = 0;
        needEnergy = 3;
        productEnergy = 21;
        needOrganic = 5;
        productOrganic = 0;
        timer = 1.0f;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            TryUpdate();
            timer = 1.0f;
        }
    }
    // Update is called once per frame
    private void TryUpdate()
    {
        CellEnergy += getEnergy - loseEnergy;
        Organic += getOrganic - loseOrganic;
        getEnergy = 0;
        loseEnergy = 0;
        getOrganic = 0;
        loseOrganic = 0;
        if (CellEnergy < -6)
        {
            Destroy(this.gameObject);
        }
        if (CellEnergy > 0)
        {
            int lackOfEnergy = 0;

            var colliders1 = Physics2D.OverlapCircleAll(transform.position, radius * 2, 1 << LayerMask.GetMask("Cell"));
            int[] value = new int[colliders1.Length];
            int[] sort = new int[colliders1.Length];
            for (int i = 0; i < colliders1.Length; i++)
            {
                value[i] = 0;
                sort[i] = i;
                CellBase cell = colliders1[i].gameObject.GetComponent<CellBase>();
                if (cell.type == CellBase.organelleType.Chloroplast ||cell.type==CellBase.organelleType.Mitochondria) continue;
                else
                {
                    lackOfEnergy += cell.needEnergy - cell.CellEnergy;
                    for (int j = 0; j < i; j++)
                    {
                        if (value[i] > value[j])
                        {
                            int temp = value[i];
                            value[j] = value[i];
                            value[i] = temp;
                            temp = sort[i];
                            sort[i] = sort[j];
                            sort[j] = temp;
                            break;
                        }
                    }
                }
            }
            if (lackOfEnergy >= CellEnergy)
            {
                for (int i = 0; i < colliders1.Length; i++)
                {
                    if (CellEnergy - value[sort[i]] >= 0)
                    {
                        colliders1[sort[i]].gameObject.GetComponent<CellBase>().GetEnergy(value[sort[i]]);
                        CellEnergy -= value[sort[i]];
                    }
                    else if (CellEnergy > 0)
                    {
                        colliders1[sort[i]].gameObject.GetComponent<CellBase>().GetEnergy(CellEnergy);
                        CellEnergy = 0;
                    }
                }
            }
            else
            {
                for (int i = 0; i < colliders1.Length; i++)
                {
                    CellBase cell = colliders1[i].gameObject.GetComponent<CellBase>();
                    if (cell.type == CellBase.organelleType.Chloroplast) continue;
                    else
                    {
                        cell.GetEnergy(cell.needEnergy - cell.CellEnergy);
                    }
                }
                lackOfEnergy = 0;
                var colliders2 = Physics2D.OverlapCircleAll(transform.position, radius * 3, 1 << LayerMask.GetMask("Cell"));
                {
                    int[] value2 = new int[colliders2.Length];
                    int[] sort2 = new int[colliders2.Length];
                    for (int i = 0; i < colliders2.Length; i++)
                    {
                        value2[i] = 0;
                        sort2[i] = i;
                        CellBase cell = colliders2[i].gameObject.GetComponent<CellBase>();
                        if (cell.type == CellBase.organelleType.Chloroplast) continue;
                        else
                        {
                            lackOfEnergy += cell.needEnergy - cell.CellEnergy;
                            for (int j = 0; j < i; j++)
                            {
                                if (value2[i] > value2[j])
                                {
                                    int temp = value2[i];
                                    value2[j] = value2[i];
                                    value2[i] = temp;
                                    temp = sort2[i];
                                    sort2[i] = sort2[j];
                                    sort2[j] = temp;
                                    break;
                                }
                            }
                        }
                    }
                    if (lackOfEnergy > CellEnergy)
                    {
                        for (int i = 0; i < colliders2.Length; i++)
                        {
                            if (CellEnergy - value2[sort2[i]] >= 0)
                            {
                                colliders2[sort2[i]].gameObject.GetComponent<CellBase>().GetEnergy(value2[sort2[i]]);
                                CellEnergy -= value2[sort2[i]];
                            }
                            else if (CellEnergy > 0)
                            {
                                colliders2[sort2[i]].gameObject.GetComponent<CellBase>().GetEnergy(CellEnergy);
                                CellEnergy = 0;
                            }
                        }
                    }
                }
            }

        }
        if (Organic >= needOrganic)
        {
            LoseOrganic(needOrganic);
            GetEnergy(productEnergy);
        }
        if (CellEnergy > maxEnergy) CellEnergy = maxEnergy;
        if (Organic > maxOrganic) Organic = maxOrganic;
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
