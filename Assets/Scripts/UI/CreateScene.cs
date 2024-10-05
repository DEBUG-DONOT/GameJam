using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateScene : UIBase
{
    // Start is called before the first frame update
    //选择细胞的时候改这个
    public GameObject newCell;
    public GameObject Blankprefab;
    public GameObject ChloroplastPrefab;
    public GameObject MitochondriaPrefab;
    public GameObject MouthPrefab;
    public GameObject FlagellumPrefab;
    public List<Button> Buttons;
    void Start()
    {
        OnExit();
    }
    public override void OnEnter()
    {
        Time.timeScale = 0;
        state = UIState.Enter;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public override void OnExit()
    {

        newCell = null;
        state = UIState.Exit;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public  GameObject GetPrefab()
    {
        GameObject.Find("UIManager").GetComponent<UIManager>().EnterPanel(GameObject.Find("CreateScene"));

        GameObject.Find("UIManager").GetComponent<UIManager>().EnterGameScene();
        return newCell;
    }

    public void SetBlank()
    {
        newCell=Blankprefab;
    }
    public void SetChlo()
    {
        newCell = ChloroplastPrefab;
    }
    public void SetMouth() 
    {
        newCell = MouthPrefab;
    }
    public void SetMito()
    {
        newCell = MitochondriaPrefab;
    }
    public void SetFlagellum() 
    {
        newCell = FlagellumPrefab;
    }
}
