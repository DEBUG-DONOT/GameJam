using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : UIBase
{
    public static PauseMenu GetInstance;
    private void Awake()
    {
        GetInstance = this;
    }
    public GameObject currScene;
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
    public override void OnEnter()
    {
        Timer.GetInstance.isCount = false;
        Time.timeScale = 0;
        state = UIState.Enter;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public override void OnExit()
    {
        Timer.GetInstance.isCount = true;
        Time.timeScale = 1;
        state = UIState.Exit;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
