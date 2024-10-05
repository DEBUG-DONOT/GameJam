using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public  GameObject cell;
    // Start is called before the first frame update
    
    void Start()
    {
        currCellPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 spawnCellPosition=new Vector3(1,0,0)+ currCellPosition;
            currCellPosition=spawnCellPosition;
            var temp= Instantiate(cell,spawnCellPosition,transform.rotation) ;
            temp.transform.parent=this.transform ;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private Vector3 currCellPosition;

}
