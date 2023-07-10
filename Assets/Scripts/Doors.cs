using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [HideInInspector]
    public bool isEnter;

    [HideInInspector]
    public bool isCloseDoor;

    public bool hasNotKnob;

    [HideInInspector]
    public Animator anim;
    
    [HideInInspector]
    public bool isOpenAnyware;
    
    

    private void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
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
