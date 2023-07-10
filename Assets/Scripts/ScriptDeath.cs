using System.Collections;
using UnityEngine;

public class ScriptDeath : MonoBehaviour
{
    [SerializeField] private GameObject mannequinDeath;
    [SerializeField] private GameObject blackScreenDeath;
    [SerializeField] private AudioSource jumpscare;
    [SerializeField] private AudioSource impactDeath;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(gameOver());
        }
    }

    IEnumerator gameOver()
    {
        jumpscare.Play();
        mannequinDeath.SetActive(true);
        yield return new WaitForSeconds(.5f);
        impactDeath.Play();
        blackScreenDeath.SetActive(true);
    }
}
