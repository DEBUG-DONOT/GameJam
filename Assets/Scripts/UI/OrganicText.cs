using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrganicText : MonoBehaviour
{
    public Text text;
    void Update()
    {
        text.text = "Organic: " + Player.GetInstance.AllOrganic.ToString();
    }
}
