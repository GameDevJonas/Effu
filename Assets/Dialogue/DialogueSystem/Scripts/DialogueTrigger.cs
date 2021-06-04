using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [HideInInspector] public DialogueManager manager;

    [Header("Dialogue ScriptableObject")]
    public Dialogue dialogue;

    [Space(40)]
    [Header("Events to Invoke after dialogue is done, if needed")]
    public UnityEvent finishedDialogueEvents;

    private void Awake()
    {
        manager = FindObjectOfType<DialogueManager>();
    }

    //Trigger my dialogue
    [ContextMenu("Trigger dialogue")]
    public void TriggerDialogue()
    {
        manager.StartDialogue(dialogue, this);
    }
}
