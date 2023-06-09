using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorRaycast : MonoBehaviour
{
    [SerializeField] private int rayLenght = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludedLayerMask = null; 

    private Doors raycastedObj;

    [SerializeField] private KeyCode interactDoorKey = KeyCode.Mouse0;
    [SerializeField] private Image crossHair = null;
    private bool isCrosshairActivate;
    private bool doOnce;

    private const string interactibleTag = "InteractiveObject";

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludedLayerMask) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLenght, mask))
        {
            if (hit.collider.CompareTag(interactibleTag))
            {
                if (!doOnce)
                {
                    raycastedObj = hit.collider.gameObject.GetComponentInParent<Doors>();
                    CrossHairChange(true);
                }

                isCrosshairActivate = true;
                doOnce = true;
                
                if (Input.GetKeyDown(interactDoorKey))
                {
                    raycastedObj = hit.collider.gameObject.GetComponentInParent<Doors>();
                    if (raycastedObj.isOpenAnyware && raycastedObj.isCloseDoor)
                    {
                        raycastedObj.isOpenAnyware = false;
                        raycastedObj.anim.SetTrigger("CloseTrigger");
                    }
                    else if(!raycastedObj.isOpenAnyware && raycastedObj.isCloseDoor)
                    {
                        raycastedObj.isOpenAnyware = true;
                        raycastedObj.anim.SetTrigger("OpenTrigger");
                    }
                }
            }
        }
        else
        {
            if (isCrosshairActivate)
            {
                CrossHairChange(false);
                doOnce = false;
            }
        }
    }

    void CrossHairChange(bool on)
    {
        if (on && !doOnce)
        {
            if (raycastedObj != null)
            {
                if (raycastedObj.isCloseDoor)
                {
                   // crossHair.color = Color.red; 
                }
                else
                {
                   // crossHair.color = Color.white;
                   // isCrosshairActivate = false;
                }
            }
        }
        else
        {
           // crossHair.color = Color.white;
            //isCrosshairActivate = false;
        }
    }
}
