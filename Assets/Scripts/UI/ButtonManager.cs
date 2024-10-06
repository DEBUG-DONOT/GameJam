using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject EndScene;
    public GameObject MainMenu;
    public GameObject PauseMenu;
    public void Onylt()
    {
        CreateScene.GetInstance.CreateYLT();
    }
    public void Onxlt()
    {
        CreateScene.GetInstance.CreateXLT();
    }
    public void OnMouth()
    {
        CreateScene.GetInstance.CreateMOUTH();
    }
    public void Onbianmao()
    {
        CreateScene.GetInstance.CreateBIANMAO();
    }
    public void OnShell()
    {
        CreateScene.GetInstance.CreateShell();
    }
    public void OnYePao()
    {
        CreateScene.GetInstance.CreateYePao();
    }
    public void OnCellSpine()
    {
        CreateScene.GetInstance.CreateCellSpine();
    }
}
