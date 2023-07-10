using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    [SerializeField] private float currentTime;

    [SerializeField] private Animator fadeAnim;
    [SerializeField] private Animator fadeAnim2;
    

    private float timerLimit = 0;
    
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject timerGO;
    [SerializeField] private PlayerMovement player;
    private GameObject fade2GO;

    private void Start()
    {
        fade2GO = fadeAnim2.gameObject;
    }

    private bool doOnce = false;

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0.5 && !doOnce)
        {
            timerText.color = Color.red;
            StartCoroutine(gameOver());
            doOnce = true;
        }
        if (currentTime <= timerLimit)
        {
            currentTime = timerLimit;
            setTimerText();
            enabled = false;
        }
        setTimerText();
    }
    private void setTimerText()
    {
        timerText.text = currentTime.ToString("0");
    }
    public IEnumerator gameOver()
    { 
        fadeAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        timerGO.SetActive(false);
        yield return new WaitForSeconds(2f);
        fade2GO.SetActive(true);
        fadeAnim2.SetTrigger("FadeOut");
        gameOverScreen.SetActive(true);
        button.SetActive(true);
        player.isDead = true;

    }

    public void RestartButton()
    {
        StartCoroutine(restartCoroutine());
    }

    IEnumerator restartCoroutine()
    {
        fadeAnim2.SetTrigger("FadeIn");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(sceneName);
    }
}