using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public Animator fadeAnim;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            changeScene();
        }
    }

    IEnumerator fade()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName);
    }

    public void changeScene()
    {
        fadeAnim.SetTrigger("FadeIn");
        StartCoroutine(fade());
    }
}
