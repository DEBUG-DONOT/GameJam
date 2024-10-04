using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UIState
{
    Enter,Exit
}
public class UIBase : MonoBehaviour
{
    public UIState state;
    public CanvasGroup canvasGroup;
    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }
    public void OnEnter()
    {
        Time.timeScale = 0;
        state=UIState.Enter;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public void OnExit()
    {
        state = UIState.Exit;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
