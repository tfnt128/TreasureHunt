using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [HideInInspector]
    public bool isEnter;

    public bool isCloseDoor;

    [HideInInspector]
    public Animator anim;
    
    public bool isOpenAnyware;

    private void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Debug.Log(isCloseDoor);
        if (isEnter)
        {
            anim.SetBool("isEnter", true);
        }
        else
        {
            anim.SetBool("isEnter", false);
        }
    }
}
