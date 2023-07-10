using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItensManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> itensList = new List<GameObject>();
    [SerializeField] private GameObject objetoParaInstanciar;

    public int collectibleCount;
    [SerializeField] private BoxCollider col;

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

    private bool doOnce = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            collectibleCount = 3;
        }
        
        if (collectibleCount >= 4 && !doOnce)
        {
            col.enabled = true;
            
            collectibleCount = 0;
            doOnce = true;
        }
    }
}