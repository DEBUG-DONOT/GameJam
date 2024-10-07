using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]private string sceneName;
    //获得场景名
    private void Awake()
    {
        sceneName = "SampleScene";
    }
    public void GetSceneName(string name)
    {
        if (SceneManager.GetSceneByName(name).IsValid())
            Debug.LogError(name + " is not a scene!");
        sceneName = name;
    }
    //换场景
    public void SwitchToScene()
    {
        if (SceneManager.GetSceneByName(name).IsValid())
            Debug.LogError("missing a scene!");
        //暂时不考虑重复进入场景，player加个dontdestroyonload
        SceneManager.LoadScene(sceneName);
    }
}