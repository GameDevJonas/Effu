using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dialogue", menuName = "BeanAcres/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    public Character character;
    public SentenceElements[] dialogue;
}

[System.Serializable]
public class SentenceElements
{
    [TextArea(3, 10)]
    public string sentence;
    public DialogueManager.Mood mood;

    public SentenceElements(string cSentence, DialogueManager.Mood currentMood)
    {
        this.sentence = cSentence;
        this.mood = currentMood;
    }
}
