using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [HideInInspector] public DialogueManager manager;

    [Header("Dialogue ScriptableObject")]
    public Dialogue dialogue;

    [Header("Events to Invoke after dialogue is done, if needed")]
    [Space(40)]
    public UnityEvent finishedDialogueEvents;

    private void Awake()
    {
        manager = FindObjectOfType<DialogueManager>();
    }

    //Trigger my dialogue
    public void TriggerDialogue()
    {
        manager.StartDialogue(dialogue, this);
    }
}
