using System;
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
    public int year;
    private Text text;
    private void Start()
    {
        timer = 0;
        isCount=false;
        text=GetComponent<Text>();
    }
    private void Update()
    {
        Debug.Log(Timer.GetInstance.isCount);
        if (Timer.GetInstance.isCount) 
        { 
            timer += Time.deltaTime;
        }
        year = (int)Math.Round(timer);
        text.text = "Time:  Year "+year.ToString();
    }
}