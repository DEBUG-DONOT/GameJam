using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject currUIPanel;
    public bool isEnd;

    private void Awake()
    {
        currUIPanel = GameObject.Find("MainMenu");
        isEnd = false;
    }
    public void EnterOtherPanel(GameObject otherPanel)
    {
        UIBase newUI = otherPanel.GetComponent<UIBase>();
        if (newUI == null)
        {
            Debug.LogError(" has no UI script!");
        }
        if (newUI.state == UIState.Exit)
        {
            currUIPanel.GetComponent<UIBase>().OnExit();
            currUIPanel=otherPanel;
            currUIPanel.GetComponent<UIBase>().OnEnter();
        }
    }
    public void EnterGameScene()
    {
        currUIPanel.GetComponent<UIBase>().OnExit();
        currUIPanel = null;
        Time.timeScale = 1.0f;
    }
    void Update()
    {
        if (currUIPanel == null||currUIPanel==GameObject.Find("CreateScene")) 
        {
            //前面写结束条件，转到结束界面
            if(Input.GetKeyDown(KeyCode.F))//写结束条件
            {
                Time.timeScale = 0;
                currUIPanel = GameObject.Find("EndScene");
                currUIPanel.GetComponent<UIBase>().OnEnter();
                isEnd=false;
                return;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                currUIPanel = GameObject.Find("PauseMenu");
                currUIPanel.GetComponent <UIBase>().OnEnter();
            }
        }
    }
}
