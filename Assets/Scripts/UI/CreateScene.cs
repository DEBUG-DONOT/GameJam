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

    public GameObject Cell;
    public Vector3 position;
    public static CreateScene GetInstance()
    {
        if (createscene == null)
        {
            createscene = new CreateScene();
        }
        return createscene;
    }
    private CreateScene() { }
    private static CreateScene createscene = null;
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
    public  void GetPositon(GameObject cell,Vector2 pos)
    {
        GameObject.Find("UIManager").GetComponent<UIManager>().EnterPanel(GameObject.Find("CreateScene"));
        Cell = cell;
        position = pos;
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
