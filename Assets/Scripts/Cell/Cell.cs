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
        rendererSize=GetComponent<Renderer>().bounds.size.x*math.sqrt(3.0f)/2.0f;
        //neighbors = new List<GameObject>(new GameObject[6]);
    }
    private void Awake()
    {
        neighbors = new List<GameObject>(new GameObject[6]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nowSpwn = true;
            //Player.GetInstance().canMove = false;
            //Debug.Log("space");
            GenerateVirtualCells();
            pauseGame();
            Debug.Log(this.gameObject.name + " space!");
        }
        if (nowSpwn)
        {
           //找到点击的位置
           var clickedGO=CheckClick.CheckClickOnSomething();
           if(clickedGO==null)
           {
                //Debug.Log("click nothing!");  
           }
           else if (virtualCell.CompareTag(clickedGO.tag)==true)
            {
                Vector3 pos=clickedGO.transform.position;   
                DestroyAllVirtualCell();
                GenerateRealCell(pos);
                pauseGame();
            }
        }
    }

    public void DestroyAllVirtualCell()
    {
        GameObject[] allVirtualCells = GameObject.FindGameObjectsWithTag(virtualCell.tag);
        foreach (GameObject virtualCell in allVirtualCells)
        {
            Destroy(virtualCell);
        }
    }
    public void GenerateRealCell(Vector3 position)//在position位置生成一个real cell
    {
        GameObject temp = Instantiate(RealCell,  position, transform.rotation);
        temp.transform.parent = transform;
        nowSpwn = false;
        //Debug.Log("generate real");
        //将新生成的实例记录在数组中
        var generateDirection = position - this.transform.position;
        int index= HexagonDirection.GetIndex(generateDirection);
        if (index == -1) Debug.LogError("can not to find index!");
        else
        {
            this.neighbors[index] = temp;
        }
        //向六个方向判断并记录新生成的实例的周围的情况
        Cell tempCell=temp.GetComponent<Cell>();
    }

    void GenerateVirtualCells()//生成所有可能的位置
    {
        Debug.Log(this.gameObject.name + "gen virtual");
        for (int i = 0; i < 6; i++)
        {
            if (neighbors[i] == null)
            {
                ShowSingleVirtualCell(i);
            }
        }
    }
    void ShowSingleVirtualCell(int i)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(HexagonDirection.Heax_Directions[i] + transform.position, Vector2.zero, 0.1f);
        if (hit2D == true)
        {
            neighbors[i] = hit2D.collider.gameObject;
            return; 
        }
        GameObject temp = Instantiate(virtualCell,HexagonDirection.Heax_Directions[i]+transform.position,transform.rotation);
        temp.transform.parent = transform;
    }
    void pauseGame()
    {
        if(gamePaused)
        {
            Time.timeScale = 0;
            gamePaused = true;
        }
        else
        {
            Time.timeScale = 1;
            gamePaused = false;
        }
    }


    private void FixedUpdate()
    {
        
    }
    float rendererSize = 0;
    private bool gamePaused=false;
    private Vector3 LeftPosition,RightPosition,UpPosition,DownPosition;
    bool nowSpwn=false;
    
    public List<GameObject> neighbors ;
    
}
