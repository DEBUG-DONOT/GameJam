using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : UIBase
{
    [SerializeField] private bool startEnter;
    // Start is called before the first frame update
    void Start()
    {
        OnExit();
        startEnter = false;
    }
    new public void OnEnter()
    {
        startEnter = true;
    }
    private void Update()
    {
        if (startEnter)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            startEnter = false;
        }
    }
}
