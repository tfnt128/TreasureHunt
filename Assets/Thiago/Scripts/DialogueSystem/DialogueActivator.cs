using UnityEngine;

public class DialogueActivator : MonoBehaviour, Interectible
{
    [SerializeField] private DialogueObject dialogueObject;

    public void UpdateDialogueObejct(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerDialogue player))
        {
            player.interectible = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerDialogue player))
        {
            if(player.interectible is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.interectible = null;
            }
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
}