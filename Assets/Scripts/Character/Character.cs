using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator != null) Debug.LogError(gameObject.name + "no animator!");
        _transform = transform;
        if (_transform != null) Debug.LogError(gameObject + "no transfomation!");
    }

    // Update is called once per frame
    void Update()
    {

    }
    //注意动作和动画的联动
    //enimy、npc都可以从character派生，在character中可以完成一部分的工作

    Transform _transform = null;
    Animator _animator = null;

}
