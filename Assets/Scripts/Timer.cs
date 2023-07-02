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
    

    private float timerLimit = 0;

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0.5)
        {
            timerText.color = Color.red;
            StartCoroutine(restart());
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
    IEnumerator restart()
    {
       // timesUpText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}