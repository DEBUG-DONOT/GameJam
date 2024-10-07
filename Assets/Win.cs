using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    bool checkWin = false;
    void Update()
    {
        if (Player.GetInstance.transform.position.x >= 230)
        {
            UIManager.GetInstance.EnterPanel(GameObject.Find("EndScene"));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Cell")
        {
            Player.GetInstance.rb.velocity=(GameObject.Find("Destination").transform.position-Player.GetInstance.transform.position)*3;
            Player.GetInstance.rb.drag = 0;
            Player.GetInstance.enabled = false;
        }
    }
}
