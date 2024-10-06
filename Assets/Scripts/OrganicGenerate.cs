using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject WeedPrefab;
    public GameObject StarPrefab;
    static float timer = 7f;
    private enum Organic
    {
        Weed,Star
    }
    public float maxTimer;
    public float minTimer;
    private Organic organic;
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
    private void TryGenerate()
    {
        
        int sighx = Random.Range(0, 2);
        int sighy = Random.Range(0, 2);
        int x, y;
        if (sighx == 0)
        {
            x = -Random.Range(20, 50);
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
        Vector3 dir = new Vector3(x, y, 0);
        if(Random.value>0.7)
            Instantiate(StarPrefab, Player.GetInstance.transform.position + dir, transform.rotation);
        else
        {
            Instantiate(WeedPrefab, Player.GetInstance.transform.position + dir, transform.rotation);
        }
    }
}
