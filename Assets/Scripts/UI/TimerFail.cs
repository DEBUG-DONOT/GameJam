using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerFail : MonoBehaviour
{
    public static TimerFail GetInstance;
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
        if (isCount) 
        { 
            timer += Time.deltaTime;
        }
        year = (int)Math.Round(timer);
        text.text = "You survived for "+year.ToString()+" Years";

    }
}