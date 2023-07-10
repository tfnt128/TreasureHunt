using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectItem : MonoBehaviour
{
    public ItensManager itens;
    private BoxCollider col;
    private MeshRenderer mesh;
    public Material newMat;
    public bool isMain;
    public Animator fade;
    private AudioSource audio;
    public AudioSource audioButton;
    public string endingSceneName;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
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
            audio.Play();
            audioButton.Play();
            
            if (isMain)
            {
                StartCoroutine(LoadNextScene());
            }
        }
    }

    private IEnumerator LoadNextScene()
    {
        fade.SetTrigger("FadeIn");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(endingSceneName);
    }
    
}
