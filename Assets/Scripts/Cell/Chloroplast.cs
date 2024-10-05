using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chloroplast : CellBase
{
    public GameObject player;
    public GameObject AttachedMouth;
    private void Awake()
    {
        mass = 1;
        cost = 3;
        player = GameObject.Find("player");
        type = organelleType.Chloroplast;
        maxEnergy = 2;
        maxOrganic = 1;
        CellEnergy = 6;
        getEnergy = 0;
        loseEnergy = 0;
        needEnergy = 0;
        productEnergy = 12;
        productOrganic = 4;
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
    private void TryUpdate()
    {
        CellEnergy += getEnergy - loseEnergy;
        Organic += getOrganic;
        getEnergy = 0;
        loseEnergy = 0;
        getOrganic = 0;
        if (CellEnergy < -6)
        {
            if (AttachedMouth != null)
            {
                Destroy(AttachedMouth);
            }
            Destroy(this.gameObject);
        }
        if (CellEnergy > 0)
        {
            int lackOfEnergy = 0;

            var colliders1 = Physics2D.OverlapCircleAll(transform.position, radius * 100, 1 << LayerMask.NameToLayer("Cell"));
            Debug.Log(colliders1.Length);
            int[] value = new int[colliders1.Length];
            int[] sort = new int[colliders1.Length];
            for (int i = 0; i < colliders1.Length; i++)
            {
                value[i] = 0;
                sort[i] = i;
                CellBase cell = colliders1[i].gameObject.GetComponent<CellBase>();
                if (cell.type == CellBase.organelleType.Chloroplast) continue;
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
        if (Organic > 0)
        {
            bool isEnough = true;
            var colliders1 = Physics2D.OverlapCircleAll(transform.position, radius * 2, 1 << LayerMask.GetMask("Cell"));
            for (int i = 0; i < colliders1.Length; i++)
            {

                CellBase cell = colliders1[i].gameObject.GetComponent<CellBase>();
                if (cell.type == CellBase.organelleType.Mitochondria)
                {
                    if (cell.Organic == 1)
                    {
                        if (Organic - cell.needOrganic >= 0)
                        {
                            cell.GetOrganic(cell.needOrganic);
                            Organic -= cell.needOrganic;
                        }
                        else if (Organic > 0)
                        {
                            cell.GetOrganic(Organic);
                            Organic = 0;
                            isEnough = false;
                        }
                    }
                }
            }
            if (isEnough)
            {
                for (int i = 0; i < colliders1.Length; i++)
                {
                    CellBase cell = colliders1[i].gameObject.GetComponent<CellBase>();
                    if (cell.type == CellBase.organelleType.Mitochondria)
                    {
                        if (cell.Organic == 1) continue;
                        else if (cell.Organic == 0)
                        {
                            if (Organic - cell.needOrganic >= 0)
                            {
                                cell.GetOrganic(cell.needOrganic);
                                Organic -= cell.needOrganic;
                            }
                            else if (Organic > 0)
                            {
                                cell.GetOrganic(Organic);
                                Organic = 0;
                                isEnough = false;
                            }
                        }
                    }
                }
                if (isEnough)
                {
                    var colliders2 = Physics2D.OverlapCircleAll(transform.position, radius * 3, 1 << LayerMask.GetMask("Cell"));
                    for (int i = 0; i < colliders2.Length; i++)
                    {
                        CellBase cell = colliders2[i].gameObject.GetComponent<CellBase>();
                        if (cell.type == CellBase.organelleType.Mitochondria)
                        {
                            if (cell.Organic == 1)
                            {
                                if (Organic - cell.needOrganic >= 0)
                                {
                                    cell.GetOrganic(cell.needOrganic);
                                    Organic -= cell.needOrganic;
                                }
                                else if (Organic > 0)
                                {
                                    cell.GetOrganic(Organic);
                                    Organic = 0;
                                    isEnough = false;
                                }
                            }
                        }
                    }
                    if (isEnough)
                    {
                        for (int i = 0; i < colliders1.Length; i++)
                        {
                            CellBase cell = colliders1[i].gameObject.GetComponent<CellBase>();
                            if (cell.type == CellBase.organelleType.Mitochondria)
                            {
                                if (cell.Organic == 1) continue;
                                else if (cell.Organic == 0)
                                {
                                    if (Organic - cell.needOrganic >= 0)
                                    {
                                        cell.GetOrganic(cell.needOrganic);
                                        Organic -= cell.needOrganic;
                                    }
                                    else if (Organic > 0)
                                    {
                                        cell.GetOrganic(Organic);
                                        Organic = 0;
                                    }
                                }
                            }
                        }
                    }

                }
            }
            if (CellEnergy > maxEnergy) CellEnergy = maxEnergy;
            if (Organic > maxOrganic) Organic = maxOrganic;
            GetEnergy(productEnergy);
            LoseEnergy(needEnergy);
            GetOrganic(productOrganic);
        }
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
