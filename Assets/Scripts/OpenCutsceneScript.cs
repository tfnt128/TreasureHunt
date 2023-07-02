using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCutsceneScript : MonoBehaviour
{
    public Transform playerPosFinal;
    public GameObject playerPos;
    public CharacterController controller;
    public CapsuleCollider col;

    public DoorRaycast raycastType;
    
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private DialogueObject NewdialogueObject;
    [SerializeField] private DialogueObject NewdialogueObject2;
    public PlayerDialogue player;
    public bool dialogueEnded = false;
    public Animator fade;
    public int cont = 0;

    public GameObject light1;
    public GameObject light2;
    public GameObject oldDinner;
    public GameObject newDinner;


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
        player.DialogueUI.ShowDialogue(dialogueObject);
    }

    IEnumerator dialogueInicialDealy()
    {
        yield return new WaitForSeconds(2f);
        Interect(player);
    }

    IEnumerator dialogueEndedDealyAction()
    {
        yield return new WaitForSeconds(2f);
        fade.SetTrigger("FadeOut");
        yield return new WaitForSeconds(8f);
        UpdateDialogueObejct(NewdialogueObject);
        Interect(player);
    }

    IEnumerator cutsceneToGame()
    {
        light1.SetActive(false);
        light2.SetActive(false);
        oldDinner.SetActive(false);
        newDinner.SetActive(true);
        yield return new WaitForSeconds(2f);
        fade.SetTrigger("FadeIn");
        yield return new WaitForSeconds(4f);
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
