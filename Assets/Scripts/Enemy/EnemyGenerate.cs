using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    public GameObject enemyGO;
    public GameObject[] enemyList;
    static float timer = 10f;
    private enum Enemy
    {
        Enemy1,Enemy2,Enemy3,Enemy4,Enemy5,Enemy6,Enemy7,Enemy8,Enemy9,Enemy10,Enemy11,Enemy12
    }
    private enum Block
    {
        leftup,leftdown, midup,middown, rightup,rightdown
    }
    public float maxTimer;
    public float minTimer;
    private Block block;
    private Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        maxTimer = 10.0f;
        minTimer = 4.0f;
    }
    bool[]hasPass = new bool[3];
    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            hasPass[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GetBlock();
            TryGenerate();
            timer = Random.Range(minTimer,maxTimer);
        }
        float currX = Player.GetInstance.transform.position.x;
        if (currX < 151 && currX > 147 && !hasPass[0])
        {
            hasPass[0] = true;
            GenerateBoss(1);
        }
        else if (currX < 252 && currX > 246 && !hasPass[1])
        {
            hasPass[1]= true;
            GenerateBoss(2);
        }
        else if(currX < 500 &&currX > 400 && !hasPass[2])
        {
            hasPass[2]= true;
            GenerateBoss(3);
        }
    }
    private void GetBlock()
    {
        if (Player.GetInstance.transform.position.x < 150)
        {
            if(Player.GetInstance.transform.position.y>40)
                block = Block.leftup;
            else
                block= Block.leftdown;
        }
        else if (Player.GetInstance.transform.position.x < 250)
        {
            if (Player.GetInstance.transform.position.y > 40)
                block = Block.midup;
            else
                block = Block.middown;
        }
        else
        {
            if (Player.GetInstance.transform.position.y > 40)
                block = Block.rightup;
            else
                block = Block.rightdown;
        }
    }
    private void TryGenerate()
    {
        switch (block)
        {
            case Block.leftup:
                enemyGO = enemyList[Random.Range(1, 5)];
                break;
            case Block.leftdown:
                enemyGO = enemyList[Random.Range(3, 5)];
                break;
            case Block.midup:
                enemyGO = enemyList[Random.Range(6, 9)];
                break;
            case Block.middown:
                enemyGO = enemyList[Random.Range(6, 9)];
                break;
            case Block.rightup:
                enemyGO = enemyList[Random.Range(10, 13)];
                break;
            case Block.rightdown:
                enemyGO = enemyList[Random.Range(10, 13)];
                break;
        }
        int sighx = Random.Range(0, 2);
        int sighy= Random.Range(0, 2);
        int x, y;
        if(sighx == 0)
        {
            x=-Random.Range(20, 50);
        }
        else
        {
            x = Random.Range(20, 50);
        }
        if (sighy == 0)
        {
            y = -Random.Range(10, 30);
        }
        else
        {
            y = Random.Range(10, 30);
        }
        Vector3 dir= new Vector3(x, y, 0);
        Instantiate(enemyGO, Player.GetInstance.transform.position+dir , transform.rotation);
    }
    private void GenerateBoss(int number)
    {
        Instantiate(enemyList[4*number], transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
    }
    //Rigidbody2D rigidbody = null;
}
