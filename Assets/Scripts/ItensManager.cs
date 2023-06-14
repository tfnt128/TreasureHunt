using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItensManager : MonoBehaviour
{
    public List<GameObject> itensList = new List<GameObject>();
    public GameObject objetoParaInstanciar;

    public int collectibleCount;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            itensList.Add(child.gameObject);
        }
        
        if (itensList.Count > 4)
        {
            ShuffleList(itensList);
            
            for (int i = 0; i < 4; i++)
            {
                GameObject filho = itensList[i];
                Instantiate(objetoParaInstanciar, filho.transform.position, filho.transform.rotation);
            }
        }
    }
    
    private void ShuffleList(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private void Update()
    {
        if (collectibleCount >= 4)
        {
            int randomChildIndex = Random.Range(0, itensList.Count);
            GameObject randomChild = itensList[randomChildIndex];
            Instantiate(objetoParaInstanciar, randomChild.transform.position, randomChild.transform.rotation);
            
            collectibleCount = 0;
        }
    }
}