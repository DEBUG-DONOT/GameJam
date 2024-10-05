using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateScene : UIBase
{
    // Start is called before the first frame update
    //Ñ¡ÔñÏ¸°ûµÄÊ±ºò¸ÄÕâ¸ö
    public GameObject newCell;
    public GameObject ShellPrefab;
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

    public void CreateYLT()
    {
        GameObject temp= Instantiate(ChloroplastPrefab,position,Cell.transform.rotation,Cell.transform);
        temp.transform.parent=Cell.transform;
        temp.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Kinematic;
        GameObject.Find("UIManager").GetComponent<UIManager>().EnterGameScene();
    }
    public void CreateXLT()
    {
        GameObject temp = Instantiate(MitochondriaPrefab, position, Cell.transform.rotation, Cell.transform);
        temp.transform.parent = Cell.transform;
        temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GameObject.Find("UIManager").GetComponent<UIManager>().EnterGameScene();
    }
    public void CreateMOUTH()
    {
        GameObject temp = Instantiate(MouthPrefab, position, Cell.transform.rotation, Cell.transform);
        temp.transform.parent = Cell.transform;
        temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GameObject.Find("UIManager").GetComponent<UIManager>().EnterGameScene();
    }
    public void CreateBIANMAO()
    {
        GameObject temp = Instantiate(FlagellumPrefab, position, Cell.transform.rotation, Cell.transform);
        temp.transform.parent = Cell.transform;
        temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GameObject.Find("UIManager").GetComponent<UIManager>().EnterGameScene();
    }
    public void CreateShell()
    {
        GameObject temp = Instantiate(ShellPrefab, position, Cell.transform.rotation, Cell.transform);
        temp.transform.parent = Cell.transform;
        temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GameObject.Find("UIManager").GetComponent<UIManager>().EnterGameScene();
    }


}
