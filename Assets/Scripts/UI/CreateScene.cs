using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateScene : UIBase
{
    // Start is called before the first frame update
    public GameObject newCell;
    public GameObject ShellPrefab;
    public GameObject ChloroplastPrefab;
    public GameObject MitochondriaPrefab;
    public GameObject MouthPrefab;
    public GameObject FlagellumPrefab;
    public GameObject YePaoPrefab;
    public GameObject CellSpinePrefab;

    public GameObject Cell;
    public Vector3 position;
    public static CreateScene GetInstance;
    private void Awake()
    {
        GetInstance = this;
    }
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
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
        SoundManager.GetInstance.Play("createCell");
    }
    public void GetPositon(GameObject cell, Vector2 pos)
    {
        GameObject.Find("UIManager").GetComponent<UIManager>().EnterPanel(GameObject.Find("CreateScene"));
        Cell = cell;
        position = pos;
    }

    public void CreateYLT()
    {
        if (Player.GetInstance.Energy >= ChloroplastPrefab.GetComponent<Chloroplast>().cost)
        {
            Player.GetInstance.Energy -= ChloroplastPrefab.GetComponent<Chloroplast>().cost;
            GameObject temp = Instantiate(ChloroplastPrefab, position, Cell.transform.rotation, Cell.transform);
            temp.transform.parent = Cell.transform;
            temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GameObject.Find("UIManager").GetComponent<UIManager>().EnterGameScene();
        }
    }
    public void CreateXLT()
    {

        if (Player.GetInstance.Energy >= MitochondriaPrefab.GetComponent<Mitochondria>().cost)
        {
            Player.GetInstance.Energy -= MitochondriaPrefab.GetComponent<Mitochondria>().cost;
            GameObject temp = Instantiate(MitochondriaPrefab, position, Cell.transform.rotation, Cell.transform);
            temp.transform.parent = Cell.transform;
            temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GameObject.Find("UIManager").GetComponent<UIManager>().EnterGameScene();
        }
    }
    public void CreateMOUTH()
    {

        if (Player.GetInstance.Energy >= MouthPrefab.GetComponent<Mouth>().cost)
        {
            Player.GetInstance.Energy -= MouthPrefab.GetComponent<Mouth>().cost;
            GameObject temp = Instantiate(MouthPrefab, position, Cell.transform.rotation, Cell.transform);
            temp.transform.parent = Cell.transform;
            temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GameObject.Find("UIManager").GetComponent<UIManager>().EnterGameScene();
        }
    }
    public void CreateBIANMAO()
    {

        if (Player.GetInstance.Energy >= FlagellumPrefab.GetComponent<Flagellum>().cost)
        {
            Player.GetInstance.Energy -= FlagellumPrefab.GetComponent<Flagellum>().cost;
            GameObject temp = Instantiate(FlagellumPrefab, position, Cell.transform.rotation, Cell.transform);
            temp.transform.parent = Cell.transform;
            temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GameObject.Find("UIManager").GetComponent<UIManager>().EnterGameScene();
        }
    }
    public void CreateShell()
    {

        if (Player.GetInstance.Energy >= ShellPrefab.GetComponent<Shell>().cost)
        {
            Player.GetInstance.Energy -= ShellPrefab.GetComponent<Shell>().cost;
            GameObject temp = Instantiate(ShellPrefab, position, Cell.transform.rotation, Cell.transform);
            temp.transform.parent = Cell.transform;
            temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GameObject.Find("UIManager").GetComponent<UIManager>().EnterGameScene();
        }
    }
    public void CreateYePao()
    {

        if (Player.GetInstance.Energy >= YePaoPrefab.GetComponent<YePao>().cost)
        {
            Player.GetInstance.Energy -= YePaoPrefab.GetComponent<YePao>().cost;
            GameObject temp = Instantiate(YePaoPrefab, position, Cell.transform.rotation, Cell.transform);
            temp.transform.parent = Cell.transform;
            temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GameObject.Find("UIManager").GetComponent<UIManager>().EnterGameScene();
        }
    }
    public void CreateCellSpine()
    {

        if (Player.GetInstance.Energy >= CellSpinePrefab.GetComponent<CellSpine>().cost)
        {
            Player.GetInstance.Energy -= CellSpinePrefab.GetComponent<CellSpine>().cost;
            GameObject temp = Instantiate(CellSpinePrefab, position, Cell.transform.rotation, Cell.transform);
            temp.transform.parent = Cell.transform;
            temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GameObject.Find("UIManager").GetComponent<UIManager>().EnterGameScene();
        }
    }

}
