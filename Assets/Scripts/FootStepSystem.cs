using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FootStepSystem : MonoBehaviour
{
    [Range(0, 20)] 
    public float frequency = 10.0f;

    private float Sin;

    private bool isTriggered = false;
    
    private PlayerMovement player;

    public AudioSource step;

    public bool isMain;
    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }


    void Update()
    {
        float inputMagnitude = player.currentInput.magnitude;

        if (inputMagnitude > 0)
        {
            StartFootStep();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            step.Play();
        }
    }

    private void StartFootStep()
    {
        Sin = Mathf.Sin(Time.time + frequency);

        
        if (!isTriggered)
        {
            float a = Random.Range(.8f, 1.2f);
            step.pitch = a;
            step.Play();
            isTriggered = true;
            StartCoroutine(delayStep());
        }

    }
    

    IEnumerator delayStep()
    {
        if (isMain)
        {
            float a = Random.Range(.7f, .9f);
            yield return new WaitForSeconds(a);
        }
        else
        {
            float a = Random.Range(.5f, .7f);
            yield return new WaitForSeconds(a);
        }
        
        
        isTriggered = false;
    }
}
