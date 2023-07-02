using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mannequinManager : MonoBehaviour
{
    public GameObject mannequin1;
    public GameObject mannequin2;
    public bool appers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (appers)
            {
                mannequin1.SetActive(true);
                if (mannequin2 != null)
                {
                    mannequin1.SetActive(true);
                }
            }
            else
            {
                mannequin1.SetActive(false);
                if (mannequin2 != null)
                {
                    mannequin1.SetActive(false);
                }
            }
        }
    }
}