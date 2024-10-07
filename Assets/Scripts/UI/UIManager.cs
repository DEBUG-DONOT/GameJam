using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject currUIPanel;
    public bool isEnd;
    public static UIManager GetInstance;
    private void Awake()
    {
        GetInstance = this;
        currUIPanel = GameObject.Find("MainMenu");
        isEnd = false;
    }
    public void EnterPanel(GameObject otherPanel)
    {
        UIBase newUI = otherPanel.GetComponent<UIBase>();
        if (newUI == null)
        {
            Debug.LogError(" has no UI script!");
        }
        if (newUI.state == UIState.Exit)
        {
            if (currUIPanel != null) currUIPanel.GetComponent<UIBase>().OnExit();
            currUIPanel = otherPanel;
            currUIPanel.GetComponent<UIBase>().OnEnter();
            Debug.LogWarning("444");
        }
        Time.timeScale = 0;
    }
    public void EnterGameScene()
    {
        Time.timeScale = 1;
        Timer.GetInstance.isCount = true;
        currUIPanel.GetComponent<UIBase>().OnExit();
        currUIPanel = null;
    }
    void Update()
    {
        if (currUIPanel == null&&Time.timeScale!=0)
        {
            //前面写结束条件，转到结束界面
            if (Input.GetKeyDown(KeyCode.F))//写结束条件
            {
                Time.timeScale = 0;
                currUIPanel = GameObject.Find("EndScene");
                currUIPanel.GetComponent<UIBase>().OnEnter();
                isEnd = false;
                return;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                currUIPanel = GameObject.Find("PauseMenu");
                currUIPanel.GetComponent<UIBase>().OnEnter();
            }
        }
    }
}
