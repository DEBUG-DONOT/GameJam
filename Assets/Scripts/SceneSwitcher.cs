using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]private string sceneName;
    //获得场景名
    public void GetSceneName(string name)
    {
        if (SceneManager.GetSceneByName(name).IsValid())
            Debug.LogError(name + " is not a scene!");
        sceneName = name;
    }
    //转换场景
    public void SwitchToScene()
    {
        if (SceneManager.GetSceneByName(name).IsValid())
            Debug.LogError("missing a scene name!");
        //先不考虑重复,player加个DontDestoryOnLoad
        SceneManager.LoadScene(sceneName);
    }
}