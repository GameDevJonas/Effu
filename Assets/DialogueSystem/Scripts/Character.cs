using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "BeanAcres/Character", order = 2)]
public class Character : ScriptableObject
{
    public string characterName;
    public GameObject portraitPrefab;
    public List<AudioClip> normalVoice = new List<AudioClip>();
    public List<AudioClip> happyVoice = new List<AudioClip>();
    public List<AudioClip> sadVoice = new List<AudioClip>();
}
