using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScene : UIBase
{
    // Start is called before the first frame update
    void Start()
    {
        OnExit();
    }
    public override void OnEnter()
    {
        Time.timeScale = 0;
        state = UIState.Enter;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public override void OnExit()
    {
        Time.timeScale = 1;
        state = UIState.Exit;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
