using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Enter,Exit
}
 public class UIBase:MonoBehaviour
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
    public virtual void OnEnter()
    {
        
    }
    public virtual void OnExit()
    {
        
    }
}
