using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject CreateScene;
    public GameObject EndScene;
    public GameObject MainMenu;
    public GameObject PauseMenu;
    public void Onylt()
    {
        GameObject.Find("CreateScene").GetComponent<CreateScene>().CreateYLT();
    }
    public void Onxlt()
    {
        GameObject.Find("CreateScene").GetComponent<CreateScene>().CreateXLT();
    }
    public void OnMouth()
    {
        GameObject.Find("CreateScene").GetComponent<CreateScene>().CreateMOUTH();
    }
    public void Onbianmao()
    {
        GameObject.Find("CreateScene").GetComponent<CreateScene>().CreateBIANMAO();
    }
    public void OnShell()
    {
        GameObject.Find("CreateScene").GetComponent<CreateScene>().CreateShell();
    }
    
}
