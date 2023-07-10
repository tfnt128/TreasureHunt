using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectItem : MonoBehaviour
{
    private ItensManager itens;
    private BoxCollider col;
    private MeshRenderer mesh;
    [SerializeField] private Material newMat;
    [SerializeField] private bool isMain;
    [SerializeField] private Animator fade;
    [SerializeField] private string endingSceneName;
    [SerializeField] private AudioSource audioButton;
    private AudioSource audio;
    
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
