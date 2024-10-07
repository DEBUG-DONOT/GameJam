using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinScene : UIBase
{
    public static WinScene GetInstance;
    public GameObject[] CloseList;
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
    private string winyear;
    public override void OnEnter()
    {
        Debug.LogWarning("111");
        foreach (var gameObject in CloseList) 
        { 
            gameObject.SetActive(false);
        }
        Time.timeScale = 0;
        state=UIState.Enter;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        winyear=Timer.GetInstance.year.ToString();
        Debug.LogWarning("222");
        text.text = "You have survived for "+ winyear + " Year"+"\n Back";
        Debug.LogWarning("333");
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
