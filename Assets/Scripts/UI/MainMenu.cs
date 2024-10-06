using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class MainMenu : UIBase 
{
    public static MainMenu GetInstance;
    private void Awake()
    {
        GetInstance = this;
    }
    public Text text;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        OnEnter();
    }
    public override void OnEnter()
    {
        text.GetComponent<EnergyText>().enabled = false;
        text.text=" ";
        state = UIState.Enter;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public override void OnExit()
    {
        text.GetComponent<EnergyText>().enabled = true;
        state = UIState.Exit;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        BGMManager.GetInstance.Play("bgm");
    }
}
