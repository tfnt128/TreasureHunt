using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    public OpenCutsceneScript dialogueActivator;

    public bool isOpen { get; private set; }

    private ResponseHadler responseHadler;
    private TypewriterEffect typewriterEffect;
    private bool hasTimer;
    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHadler = GetComponent<ResponseHadler>();

      //  CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject, bool hasTimer)
    {
        isOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughtDialogue(dialogueObject, hasTimer));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHadler.AddResponseEvents(responseEvents);
    }
    
    private IEnumerator StepThroughtDialogue(DialogueObject dialogueObject, bool hasTimer)
    {

        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {


            string dialogue = dialogueObject.Dialogue[i];

            yield return runTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;



            yield return new WaitForSeconds(.5f);
            if (!hasTimer)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }
            
        }

        if (dialogueObject.HasResponses)
        {
            responseHadler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
    }

    private IEnumerator runTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.isRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.E))
            {
                typewriterEffect.typewriterSpeed = 100f;
                typewriterEffect.isFaster = true;
            }
        }

    }

    public void CloseDialogueBox()
    {
        textLabel.text = string.Empty;
        StartCoroutine(dialogueBoxFadeOut());
        if (dialogueActivator != null)
        {
            dialogueActivator.dialogueEnded = true;
        }
    }
    private IEnumerator dialogueBoxFadeOut()
    {
        yield return new WaitForSeconds(1f);
        isOpen = false;
        dialogueBox.SetActive(false);
    }
}
