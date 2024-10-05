using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //private static SoundManager instance = null;  可能用到
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    //sound同时可能播放多次
    public void Play(string name)
    {
        AudioClip audioClip= Resources.Load<AudioClip>("Sound/" + name);
        if (audioClip == null)
        {
            Debug.LogError(name + " is not a sound!");
        }
        audioSource.PlayOneShot(audioClip);
    }
    /*public static SoundManager Instance
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
        if(instance == null)
        {
            instance = new SoundManager();
        }
    }*/
}