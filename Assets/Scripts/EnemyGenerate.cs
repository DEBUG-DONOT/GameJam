using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    public GameObject enemyGO;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
      //  rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 0;i<EnemyNumber;i++)
        {
            Instantiate(enemyGO,transform.position+ new Vector3(0f,1f,0f),transform.rotation);
        }
    }
    public int EnemyNumber = 1;
    //Rigidbody2D rigidbody = null;
}
