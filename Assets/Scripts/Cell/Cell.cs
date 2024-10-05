using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public  GameObject cell;
    // Start is called before the first frame update
    
    void Start()
    {
        LeftPosition = new Vector3(1, 0, 0);
        RightPosition=  new Vector3(-1,0, 0);
        UpPosition=  new Vector3(0,1,0);
        DownPosition=  new Vector3(0,1,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            nowSpwn = true;
            //pauseGame();
        }
        if (nowSpwn)
        {
            GameObject temp;
            Vector3 spawnPosition=Vector3.zero;
            if(Input.GetKeyDown (KeyCode.W))
            { 
                spawnPosition=UpPosition;
                UpPosition += new Vector3(0, 1, 0);
                //pauseGame();
                Debug.Log("W");
                temp = Instantiate(cell, spawnPosition + this.transform.position, transform.rotation);
                temp.transform.parent = transform;
                nowSpwn = false;
            }
            else if(Input.GetKeyDown (KeyCode.S))
            {
                spawnPosition = DownPosition;
                DownPosition += new Vector3(0, -1, 0);
                //pauseGame();
                Debug.Log("S");
                temp = Instantiate(cell, spawnPosition + this.transform.position, transform.rotation);
                temp.transform.parent = transform;
                nowSpwn = false;
            }
            else if(Input.GetKeyDown (KeyCode.D))
            {
                spawnPosition=LeftPosition;
                LeftPosition += new Vector3(1, 0, 0);
                //pauseGame();
                Debug.Log("D");
                temp = Instantiate(cell, spawnPosition + this.transform.position, transform.rotation);
                temp.transform.parent = transform;
                nowSpwn = false;
            }
            else if(Input.GetKeyDown (KeyCode.A))
            {
                spawnPosition=RightPosition;
                RightPosition += new Vector3(-1, 0, 0);
                //pauseGame();
                Debug.Log("A");
                temp = Instantiate(cell, spawnPosition + this.transform.position, transform.rotation);
                temp.transform.parent = transform;
                nowSpwn = false;
            }
        }
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
    private bool gamePaused=false;
    private Vector3 LeftPosition,RightPosition,UpPosition,DownPosition;
    bool nowSpwn=false;
}
