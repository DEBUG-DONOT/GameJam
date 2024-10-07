using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    //private static BGMManager instance=null; 可能用到
    private AudioSource audioSource;
    private string currBGMName=null;
    public static BGMManager GetInstance;
    private void Awake()
    {
        GetInstance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }
    //BGM同时只播放一首
    public void Play(string name)
    {
        if (!name.Equals(currBGMName))
        {
            Stop();
            audioSource.clip =  Resources.Load<AudioClip>("BGM/" + name);
            if(audioSource.clip== null)
            {
                Debug.LogError(name+" is not a bgm!");
            }
            currBGMName = name;
            audioSource.Play();
        }
    }
    public void Stop() 
    {
        audioSource.Stop();
        currBGMName =null;
    }
    /*public static BGMManager Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }*/
}
