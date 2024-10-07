using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    public GameObject enemyGO;
    public GameObject[] enemyList;
    static float timer = 10f;
    private enum Block
    {
        left, mid, right
    }
    public float maxTimer;
    public float minTimer;
    private Block block;
    private Enemy enemy;
    public GameObject left;
    public GameObject midleft;
    public GameObject midright;
    public GameObject right;
    // Start is called before the first frame update
    void Start()
    {
        maxTimer = 10.0f;
        minTimer = 4.0f;
    }
    bool[] hasPass = new bool[3];
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
            TryGenerate();
            timer = Random.Range(minTimer, maxTimer);
        }
        float currX = Player.GetInstance.transform.position.x;
        if (currX < 151 && currX > 147 && !hasPass[0])
        {
            hasPass[0] = true;
            GenerateBoss(1);
        }
        else if (currX < 252 && currX > 246 && !hasPass[1])
        {
            hasPass[1] = true;
            GenerateBoss(2);
        }
        else if (currX < 500 && currX > 400 && !hasPass[2])
        {
            hasPass[2] = true;
            GenerateBoss(3);
        }
    }
    private void GetBlock(float x)
    {
        if (x < midleft.transform.position.x)
        {
            block = Block.left;
        }
        else if (x < midright.transform.position.x)
        {
            block = Block.mid;
        }
        else
        {
            block = Block.right;
        }
    }
    private void TryGenerate()
    {
        float x, y;
        do
        {
            int sighx = Random.Range(0, 2);


            if (sighx == 0)
            {
                x = Player.GetInstance.transform.position.x - Random.Range(20, 50);
            }
            else
            {
                x = Player.GetInstance.transform.position.x + Random.Range(20, 50);
            }
            GetBlock(x + Player.GetInstance.transform.position.x);
            int sighy = Random.Range(0, 2);
            if (sighy == 0)
            {
                y = Player.GetInstance.transform.position.x - Random.Range(10, 30);
            }
            else
            {
                y = Player.GetInstance.transform.position.x + Random.Range(10, 30);
            }
        }
        while (IsInBlock(x, y));
        switch (block)
        {
            case Block.left:
                int randomType = Random.Range(1, 5);
                enemyGO = enemyList[randomType];
                if (randomType >= 3)
                    y = 40;
                break;
            case Block.mid:
                randomType = Random.Range(6, 9);
                enemyGO = enemyList[randomType];
                break;
            case Block.right:
                randomType = Random.Range(10, 13);
                enemyGO = enemyList[randomType];
                break;
        }
        Vector3 dir = new Vector3(x, y, 0);
        Instantiate(enemyGO, Player.GetInstance.transform.position + dir, transform.rotation);
    }
    private bool IsInBlock(float x, float y)
    {
        switch (block)
        {
            case Block.left:
                return (x <= midleft.transform.position.x && x > left.transform.position.x + 1 && y > (0.14655 * x - 16.2416) && y < 40);
            case Block.mid:
                return (x <= midright.transform.position.x && x > midleft.transform.position.x + 1 && y > 0.10606 * x - 16.72728 && y < 40);
            case Block.right:
                return (x <= right.transform.position.x && x > midright.transform.position.x + 1 && y > 0.22 * x - 30.4 && y < 40);
            default: return false;
        }
    }
    private void GenerateBoss(int number)
    {
        Instantiate(enemyList[4 * number], transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
    }
    //Rigidbody2D rigidbody = null;
}
