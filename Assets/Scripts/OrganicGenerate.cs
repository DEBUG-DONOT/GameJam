using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganicGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject WeedPrefab;
    public GameObject StarPrefab;
    static float timer = 7f;
    private enum Block
    {
        left, mid, right
    }
    private Block block;
    public GameObject left;
    public GameObject midleft;
    public GameObject midright;
    public GameObject right;
    public float maxTimer;
    public float minTimer;
    // Start is called before the first frame update
    void Start()
    {
        maxTimer = 7.0f;
        minTimer = 2.5f;
    }
    void Update()
    {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {   
            
            TryGenerate();
            timer = Random.Range(minTimer, maxTimer);
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
            int sighy = Random.Range(0, 2);

            if (sighx == 0)
            {
                x = -Random.Range(20, 50);
            }
            else
            {
                x = Random.Range(20, 50);
            }
            GetBlock(x);
            if (sighy == 0)
            {
                y = -Random.Range(10, 30);
            }
            else
            {
                y = Random.Range(10, 30);
            }
        }
        while (IsInBlock(x, y));
        Vector3 dir = new Vector3(x, y, 0);
        if(Random.value>0.7)
            Instantiate(StarPrefab, Player.GetInstance.transform.position + dir, transform.rotation);
        else
        {
            Instantiate(WeedPrefab, Player.GetInstance.transform.position + dir, transform.rotation);
        }
    }
    private bool IsInBlock(float x, float y)
    {
        switch (block)
        {
            case Block.left:
                return(x<=midleft.transform.position.x&&x>left.transform.position.x+1&& y > (0.14655*x - 16.2416)&&y<40);
            case Block.mid:
                return (x <= midright.transform.position.x && x > midleft.transform.position.x + 1 && y > 0.10606*x - 16.72728 && y < 40);
            case Block.right:
                return (x <= right.transform.position.x && x > midright.transform.position.x + 1 && y > 0.22*x - 30.4 && y < 40);
            default:return false;
        }
    }
}
