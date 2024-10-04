using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : UIBase 
{
    private void Start()
    {
        OnEnter();
    }
    public override void OnEnter()
    {
        state = UIState.Enter;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public override void OnExit()
    {
        state = UIState.Exit;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
