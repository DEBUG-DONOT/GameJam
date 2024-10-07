using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organic : MonoBehaviour
{
    private void Start()
    {
        int min = -3;
        int max = 3;
        Vector3 randomVector = new Vector3(Random.Range(min, max), Random.Range(min,max), 0).normalized;
        GetComponent<Rigidbody2D>().velocity = randomVector *2;
    }
    private void Update()
    {
        if(Vector2.Distance(transform.position, Player.GetInstance.transform.position) >= 80)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go=collision.gameObject;
        Debug.Log(go.gameObject.name);
        //Èç¹ûÊÇmouth
        if(go.CompareTag("Mouth"))
        {
            if(this.gameObject.CompareTag("Star"))
            {
                Player.GetInstance.AllOrganic += 5;
            }
            else if(this.gameObject.CompareTag("Weed"))
            {
                Player.GetInstance.AllOrganic += 1;
            }
            SoundManager.GetInstance.Play("Eat");
            Destroy(this.gameObject);
        }
    }

}
