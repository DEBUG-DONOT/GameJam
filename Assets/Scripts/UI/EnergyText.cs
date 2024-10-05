using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyText : MonoBehaviour
{
    public Text text;
    private GameObject player;
    // Update is called once per frame
    private void Start()
    {
        player = GameObject.Find("player");
    }
    void Update()
    {
        text.text="Energy: "+ player.GetComponent<Player>().Energy.ToString();
    }
}
