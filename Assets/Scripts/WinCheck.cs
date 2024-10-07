using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheck : MonoBehaviour
{

    void Update()
    {
        if (GameObject.Find("player").transform.position.x > 335)
        {
            UIManager.GetInstance.EnterPanel(WinScene.GetInstance.gameObject);
            Debug.LogWarning("winnnn");
            Destroy(this.gameObject);
        }
    }
}
