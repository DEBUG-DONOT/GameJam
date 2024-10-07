using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRight : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
            Player.GetInstance.rb.velocity=(GameObject.Find("Destination").transform.position-Player.GetInstance.transform.position).normalized*2;
            Player.GetInstance.rb.drag = 0;
            Player.GetInstance.enabled = false;
    }
}
