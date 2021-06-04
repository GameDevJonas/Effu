using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    public SentenceElements[] dialogue;
}

[System.Serializable]
public class SentenceElements
{
    public string name;
    [TextArea(3, 10)]
    public string sentence;
    public AudioClip voiceLine;

    public SentenceElements(string cSentence, AudioClip clip, string name)
    {
        this.name = name;
        this.sentence = cSentence;
        this.voiceLine = clip;
    }
}
