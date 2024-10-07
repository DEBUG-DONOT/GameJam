using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer GetInstance;
    private void Awake()
    {
        GetInstance = this;
    }
    public bool isCount;
    public float timer;
    private Text text;
    private void Start()
    {
        timer = 0;
        isCount=false;
        text=GetComponent<Text>();
    }
    private void Update()
    {
        if (isCount) 
        { 
            timer += Time.deltaTime;
        }
        text.text = "Time: "+timer.ToString("f2");

    }
}