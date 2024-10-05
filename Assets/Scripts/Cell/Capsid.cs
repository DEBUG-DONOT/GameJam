using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsid : CellBase
{
    // Start is called before the first frame update
    void Awake()
    {
        timer = 1.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            TryUpdate();
            timer = 1.0f;
        }
    }
    private void TryUpdate()
    {

    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
