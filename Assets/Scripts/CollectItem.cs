using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public ItensManager itens;
    private BoxCollider col;
    private MeshRenderer mesh;
    public Material newMat;
    public bool isMain;
    public Animator fade;
    
    private void Start()
    {
        if (fade != null)
        {
            fade.SetTrigger("FadeOut");
        }
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<BoxCollider>();
        itens = GameObject.FindWithTag("ItensManager").GetComponent<ItensManager>();
    }

    private bool once = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !once)
        {
            itens.collectibleCount++;
            mesh.material = newMat;
            Destroy(col);
            once = true;
            
            if (isMain)
            {
                fade.SetTrigger("FadeIn");
            }
        }
    }
}
