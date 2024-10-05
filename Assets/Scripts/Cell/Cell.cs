using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour
{
    public  GameObject RealCell;
    public GameObject virtualCell;
    // Start is called before the first frame update
    
    void Start()
    {
        rendererSize=GetComponent<Renderer>().bounds.size.x;
        //Debug.Log(rendererSize);
    }
    private void Awake()
    {
        neighbors = new List<GameObject>(new GameObject[6]);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AllGenerate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nowSpwn = true;
            GenerateVirtualCells();
            pauseGame();
        }
        if (nowSpwn)
        {
            //找到点击的位置
            var clickedGO = CheckClick.CheckClickOnSomething();
            if (clickedGO != null && virtualCell.CompareTag(clickedGO.tag) == true)
            {
                Vector3 pos = clickedGO.transform.position;
                DestroyAllVirtualCell();
                //DestroySelfVirtualCell();
                GenerateRealCell(pos);
                pauseGame();
            }
        }
    }


    public List<Vector3> ChoseMultiVirtualCell()//一次选中多个virtual cell
    {
        List<Vector3> poses=new List<Vector3>();
        //直到ui发出指令

        return poses;
    }
    public void GenerateMultiRealCell(List<Vector3> poses)//一次选中多个virtual cell并生成
    {
        DestroyAllVirtualCell();

    }

    void DestroyAllVirtualCell()
    {
        GameObject[] allVirtualCells = GameObject.FindGameObjectsWithTag(virtualCell.tag);
        foreach (GameObject virtualCell in allVirtualCells)
        {
            Destroy(virtualCell);
        }
    }

    public void GenerateRealCell(Vector3 position)//在position位置生成一个real cell
    {
        GameObject temp = Instantiate(RealCell, position, transform.rotation);
        temp.transform.parent = transform;
        nowSpwn = false;
        // 在子物体上添加 Fixed Joint 2D 组件
        temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;


    }
    public void GenerateRealCell(Vector3 position,GameObject m_prefab)//在position位置生成一个real cell
    {
        GameObject temp = Instantiate(m_prefab, position, transform.rotation);
        temp.transform.parent = transform;
        nowSpwn = false;

    }

    void GenerateVirtualCells()//生成所有可能的位置
    {
        //Debug.Log(this.gameObject.name + "gen virtual");
        for (int i = 0; i < 6; i++)
        {
            if (neighbors[i] == null)
            {
                ShowSingleVirtualCell(i);
            }
        }
    }
    public bool IsOutSideCell()//这个细胞是不是与外界有接触
    {
        for (int i = 0; i < 6; i++)
        {
            if (neighbors[i] == null)
            {
                if (testInSideOneDirection(i) == false) return true; //这个方向没东西
            }
        }
        return false;   
    }
    bool testInSideOneDirection(int i)//一个方向
    {
        RaycastHit2D hit2D = Physics2D.Raycast(HexagonDirection.Heax_Directions[i] + transform.position, Vector2.zero, 0.1f);
        if (hit2D == true)//这个方向有东西
        {
            return false;
        }
        return true;
    }
    void ShowSingleVirtualCell(int i)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(HexagonDirection.Heax_Directions[i] + transform.position, Vector2.zero, 0.1f);
        if (hit2D == true)
        {
            neighbors[i] = hit2D.collider.gameObject;
            return; 
        }
        GameObject temp = Instantiate(virtualCell,rendererSize*HexagonDirection.Heax_Directions[i]+transform.position,transform.rotation);
        temp.transform.parent = transform;
    }
    void pauseGame()
    {
        if(!gamePaused)
        {
            Time.timeScale = 0;
            gamePaused = true;
            //Debug.Log("pause!");
        }
        else
        {
            Time.timeScale = 1;
            gamePaused = false;
        }
    }

    float rendererSize = 0;
    private bool gamePaused=false;
    private Vector3 LeftPosition,RightPosition,UpPosition,DownPosition;
    bool nowSpwn=false;
    public List<GameObject> neighbors ;
    
}
