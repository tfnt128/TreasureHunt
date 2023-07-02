using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, Interectible
{
    [SerializeField] private DialogueObject dialogueObject;
    public PlayerDialogue player;
    private bool isActivate = false;

    public void UpdateDialogueObejct(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivate)
        {
            Interect(player);
            isActivate = true;
            Destroy(this.gameObject);
        }
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
    
}