using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.transform.CompareTag("Cell"))
        {
            Player.GetInstance().Energy -= damge;
        }
        Destroy(gameObject);
    }
    [SerializeField]
    int damge = 1;

}
