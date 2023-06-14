using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public ItensManager itens;

    private void Start()
    {
        itens = GameObject.FindWithTag("ItensManager").GetComponent<ItensManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            itens.collectibleCount++;
            Destroy(transform.parent.gameObject);
        }
    }
}
