using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organic : MonoBehaviour
{
    public int organic;
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (this.gameObject.CompareTag("Star"))
        {

        }
        else if (this.gameObject.CompareTag("Weed"))
        {

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go=collision.gameObject;
        //Èç¹ûÊÇmouth
        if(go.CompareTag("Mouth"))
        {
            if(this.gameObject.CompareTag("Star"))
            {
                Player.GetInstance().AllOrganic += 5;
            }
            else if(this.gameObject.CompareTag("Weed"))
            {
                Player.GetInstance().AllOrganic += 1;
            }
        }
    }

}
