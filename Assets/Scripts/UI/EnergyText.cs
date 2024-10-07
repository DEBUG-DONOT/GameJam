using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyText : MonoBehaviour
{
    public Text text;
    void Update()
    {
        text.text="Energy: \n   "+ Player.GetInstance.Energy.ToString();
    }
}
