using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour
{
    public  GameObject RealCell;
    public GameObject virtualCell;
    static public GameObject ButtonGenCell;
    // Start is called before the first frame update

    void Start()
    {
        
       
        //Debug.Log(rendererSize);
    }
    private void Awake()
    {
        neighbors = new List<GameObject>(new GameObject[6]); 
        rendererSize=virtualCell.GetComponent<Renderer>().bounds.size.x+0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(nowSpwn==true)
            {

            }
            else
            {
            nowSpwn = true;
            pauseGame();
            GenerateVirtualCells();

            }
        }
        if (nowSpwn)
        {
           
            var clickedGO = CheckClick.CheckClickOnSomething();
            if (clickedGO != null && virtualCell.CompareTag(clickedGO.tag) == true)
            {
                Debug.Log("click");
                Vector3 pos = clickedGO.transform.position;
                DestroyAllVirtualCell();
                GenerateRealCell(pos);
            }
        }
    }
 



    void DestroyAllVirtualCell()
    {
        Debug.Log("destroy");
        GameObject[] allVirtualCells = GameObject.FindGameObjectsWithTag(virtualCell.tag);
        foreach (GameObject virtualCell in allVirtualCells)
        {
            Destroy(virtualCell);
        }
    }

    public void GenerateRealCell(Vector3 position)//在position位置生成一个real cell
    {
        //GameObject temp = Instantiate(RealCell, position, transform.rotation);
        //temp.transform.parent = transform;
        nowSpwn = false;
        //在子物体上添加 Fixed Joint 2D 组件
        GameObject.Find("CreateScene").GetComponent<CreateScene>().GetPositon(gameObject, position);
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Cell"))
            {
                child.GetComponent<Cell>().nowSpwn = false;
            }
        }

        //temp.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;


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
    {   Vector3 newDirection =  transform.rotation* HexagonDirection.Heax_Directions[i] ;
        Debug.Log(HexagonDirection.Heax_Directions[i] + "   " + newDirection);
        RaycastHit2D hit2D = Physics2D.Raycast(newDirection + transform.position, Vector2.zero, 0.1f);
        if (hit2D == true)
        {
            neighbors[i] = hit2D.collider.gameObject;
            return; 
        }
        Debug.Log(gameObject.name + "gen virsual");
        
        GameObject temp = Instantiate(virtualCell,rendererSize* (newDirection.normalized) + transform.position, Player.GetInstance.transform.rotation);
        Debug.Log(rendererSize);
        temp.transform.parent = transform;
        Debug.Log("position is "+temp.transform.localPosition);
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
