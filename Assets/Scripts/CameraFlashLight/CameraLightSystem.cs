using System.Collections;
using UnityEngine;

public class CameraLightSystem : MonoBehaviour
{
    private Light light;
    private bool takePicture;
    private bool isCameraEquipped;
    [SerializeField] private float flashDuration = 1f;
    [SerializeField] private float flashIntensity = 180f;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private float fadeDelay = 0.5f;
    [SerializeField] private float fadeSpeed;
    private Animator cameraAnim;
    [SerializeField] private AudioSource audioFlash;

    private void Start()
    {
        cameraAnim = GetComponentInParent<Animator>();
        light = GetComponent<Light>();
        
        
    }

    private void Update()
    {
        fadeSpeed = flashIntensity / fadeDuration;
        if (Input.GetKeyDown(KeyCode.F) && !takePicture && isCameraEquipped)
        {
            StartCoroutine(FlashLight());
            takePicture = true;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isCameraEquipped)
            {
                cameraAnim.SetTrigger("DOWN");
            }
            else
            {
                cameraAnim.SetTrigger("UP");
            }
            isCameraEquipped = !isCameraEquipped;
        }
    }

    private IEnumerator FlashLight()
    {
        audioFlash.Play();
        light.intensity = flashIntensity;
        yield return new WaitForSeconds(flashDuration);
        
        while (light.intensity > 0)
        {
            light.intensity -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(fadeDelay);
        takePicture = false;
    }
}