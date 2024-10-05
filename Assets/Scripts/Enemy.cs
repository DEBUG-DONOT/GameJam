using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        Vector3 min = new Vector3(-10, -10, 0);
        Vector3 max = new Vector3(10, 10, 0);
        randomVector = new Vector3(Random.Range(min.x, max.x), Random.Range(min.x, max.x), Random.Range(min.x, max.x));
        randomVector.Normalize();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(Vector3.Distance(this.transform.position,Player.GetInstance().transform.position)>SearchRange)
        {

            transform.Translate(randomVector*speed*Time.deltaTime);
        }
        else
        {
            ChasePlayer();
        }
    }
    void ChasePlayer()
    {
        Vector3 direction = (Player.GetInstance().transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
    Vector3 randomVector;
    [SerializeField]
    private float speed=1.0f;
    [SerializeField]
    private float SearchRange = 5.0f;
    
}
