using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCutsceneScript : MonoBehaviour
{
    [SerializeField] private Transform playerPosFinal;
    [SerializeField] private GameObject playerPos;
    [SerializeField] private CharacterController controller;
    [SerializeField] private CapsuleCollider col;

    [SerializeField] private DoorRaycast raycastType;
    
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private DialogueObject NewdialogueObject;
    [SerializeField] private DialogueObject NewdialogueObject2;
    [SerializeField] private PlayerDialogue player;
    [SerializeField] private FootStepSystem footStep;
    
    public bool dialogueEnded = false;
    [SerializeField] private Animator fade;
    [SerializeField] private int cont = 0;

    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;
    [SerializeField] private GameObject oldDinner;
    [SerializeField] private GameObject newDinner;
    [SerializeField] private AudioSource audioDoorOpen;
    [SerializeField] private AudioSource audioDoorClose;
    [SerializeField] private AudioSource audioLightDown;
    private bool hasTimer = false;


    private void Start()
    {
        StartCoroutine(dialogueInicialDealy());
    }

    private void Update()
    {
        if (dialogueEnded && cont != 2) 
        {

                if (cont == 0)
                {
                    StartCoroutine(dialogueEndedDealyAction());
                    cont = 1;
                    dialogueEnded = false;
                }
                else
                {
                    StartCoroutine(cutsceneToGame());
                    cont = 2;
                    dialogueEnded = false;
                }
        }
    }

    public void UpdateDialogueObejct(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }
    

    public void Interect(PlayerDialogue player)
    {
        foreach(DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if(responseEvents.DialogueObject == dialogueObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        player.DialogueUI.ShowDialogue(dialogueObject, hasTimer);
    }

    IEnumerator dialogueInicialDealy()
    {
        yield return new WaitForSeconds(2f);
        Interect(player);
    }

    IEnumerator dialogueEndedDealyAction()
    {
        yield return new WaitForSeconds(2f);
        audioDoorOpen.Play();
        yield return new WaitForSeconds(1f);
        audioDoorClose.Play();
        yield return new WaitForSeconds(2f);
        fade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(8f);
        UpdateDialogueObejct(NewdialogueObject);
        Interect(player);
    }

    IEnumerator cutsceneToGame()
    {
        audioLightDown.Play();
        light1.SetActive(false);
        light2.SetActive(false);
        oldDinner.SetActive(false);
        newDinner.SetActive(true);
        yield return new WaitForSeconds(2f);
        fade.SetTrigger("FadeIn");
        yield return new WaitForSeconds(4f);
        footStep.enabled = true;
        playerPos.transform.position = playerPosFinal.position;
        col.isTrigger = false;
        fade.SetTrigger("FadeOut");
        controller.enabled = true;
        yield return new WaitForSeconds(1f);
        raycastType.isNotDoor = false;
        UpdateDialogueObejct(NewdialogueObject2);
        Interect(player);
        

    }
}
