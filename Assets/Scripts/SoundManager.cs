using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public static SoundManager Instance
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
    }
    public void Play(string name)
    {
        audioSource.clip = Resources.Load<AudioClip>("Sound/" + name);
        if (audioSource.clip == null)
        {
            Debug.LogError(name + " is not a sound!");
        }
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void Stop()
    {
        audioSource.Stop();
    }
}