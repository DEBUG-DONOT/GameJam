using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROTA : MonoBehaviour
{
    private Vector3 axis;
    private Vector3 point;
    private void Start()
    {
        point = new Vector3(0, 100, 0);
        axis= Vector3.forward;
    }
    void FixedUpdate()
    {
        transform.RotateAround(point, axis, 60 * Time.deltaTime);
    }
}
