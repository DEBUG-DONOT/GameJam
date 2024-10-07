using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndScene : UIBase
{
    public static EndScene GetInstance;
    private void Awake()
    {
        GetInstance=this;
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        OnExit();
    }
    public Text text;
    private string failyear;
    public override void OnEnter()
    {
        Time.timeScale = 0;
        state=UIState.Enter;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        failyear=Timer.GetInstance.year.ToString();
        text.text = "You have survived for "+ failyear + " Year"+"\n Back";
    }
        
    public override void OnExit()
    {
        Time.timeScale=1;
        state = UIState.Exit;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
