using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public enum Platform { computer, mobile};
    public Platform currentPlatform;

    //A bool to use if certain restrictions with dialogue is needed (such as movement etc.)
    public static bool inDialogue;

    //Speed of text appearing in-game
    private float textSpeed;

    [Header("The speed of the text when '<|> </|>' is used as tag. (Slow effect)")]
    public float slowSpeed;

    [Header("Normal speed of the dialogue text")]
    public float normalSpeed;

    //Variables to hold information about current sentence, mood and dialogue
    private Queue<string> sentences;
    private Queue<Mood> moods;
    private Dialogue currentDialogue;

    [Header("UI variables")]
    public Animator anim;
    public TextMeshProUGUI nameText, continueButtonText;
    public Transform portraitPosition;
    private Animator portraitAnimator;
    //The instantiated GameObject of the portrait
    private GameObject portraitClone;

    [Header("Script for using tags in dialogue (Sits on 'Dialogue' child of UI)")]
    public TextMeshAnimator animatorText;

    //Enum to check for dialogue moods
    public enum Mood { happy, normal, sad };
    private List<AudioClip> activeVoicePool = new List<AudioClip>();

    //Bools to check for tags, each letter in a sentence, text-skip etc.
    [HideInInspector] public bool inTag = false;
    [HideInInspector] public bool finishedSentence = false;
    private string currentSentence;
    private int index;

    [Header("Audio sources for SFX")]
    public AudioSource voiceSource;
    public AudioSource popupSource, continueSource, endSource;

    //Which trigger triggered the dialogue
    private DialogueTrigger trigger;

    private void Awake()
    {
        sentences = new Queue<string>();
        moods = new Queue<Mood>();
        textSpeed = normalSpeed;
    }

    //Changes mood/portrait/voice based on info in Dialogue ScriptableObject
    public void ChangeMood(Mood mood, Dialogue dialogue)
    {
        switch (mood)
        {
            case Mood.happy:
                activeVoicePool = dialogue.character.happyVoice;
                portraitAnimator.SetTrigger("Happy");
                break;
            case Mood.normal:
                activeVoicePool = dialogue.character.normalVoice;
                portraitAnimator.SetTrigger("Normal");
                break;
            case Mood.sad:
                activeVoicePool = dialogue.character.sadVoice;
                portraitAnimator.SetTrigger("Sad");
                break;
        }
    }

    //Starts dialogue
    public void StartDialogue(Dialogue dialogue, DialogueTrigger dTrigger)
    {
        if(currentPlatform == Platform.mobile) VibrationMethods.ShortLowVibration();

        inDialogue = true;
        trigger = dTrigger;

        //DialogueUI in
        anim.SetTrigger("In");

        //If a portrait already exists, destroy it before starting new dialogue
        if (portraitClone != null)
        {
            Destroy(portraitClone);
        }
        currentDialogue = dialogue;

        animatorText.BeginAnimation();

        //Set UI variables according to Dialogue ScriptableObject
        nameText.text = dialogue.character.characterName;
        portraitClone = Instantiate(dialogue.character.portraitPrefab, portraitPosition.transform);
        portraitAnimator = portraitClone.GetComponent<Animator>();
        switch (currentPlatform) { case Platform.computer: continueButtonText.text = "Click to continue"; break; case Platform.mobile: continueButtonText.text = "Tap to continue"; break; }

        //Clear all text, sentences and moods from queues
        animatorText.text = "";
        sentences.Clear();
        moods.Clear();

        //Queue new dialogue/mood
        foreach (SentenceElements sentenceElement in dialogue.dialogue)
        {
            sentences.Enqueue(sentenceElement.sentence);
            moods.Enqueue(sentenceElement.mood);
        }

        //Display next sentence
        DisplayNextSentence(false);
    }

    //Prepares next sentence
    public void DisplayNextSentence(bool fromButton)
    {
        //Checks if this is calles from the game or a script
        if (fromButton)
        {
            if (currentPlatform == Platform.mobile) VibrationMethods.ShortLowVibration();
            continueButtonText.GetComponent<Animator>().Play("Text_Out");
        }
        if (!finishedSentence && fromButton)
        {
            //Skips waiting time for text to appear
            StopAllCoroutines();
            animatorText.text = currentSentence;

            continueButtonText.GetComponent<Animator>().Play("Text_In");
            finishedSentence = true;
            return;
        }
        //Reset bool and text speed when new sentence is displayes
        finishedSentence = false;
        textSpeed = normalSpeed;

        //If more sentences are left continue, else end dialogue
        if (fromButton && sentences.Count > 0)
        {
            continueSource.Play();
        }
        else if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        } 

        //Write next sentence
        currentSentence = sentences.Dequeue();
        Mood mood = moods.Dequeue();
        ChangeMood(mood, currentDialogue);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    //Checks for tags, and makes sure '<', '/', '>' and tags is not written in the dialogue
    public void CheckForTags(string sentence, char character, int characterIndex)
    {
        if (character == '<')
        {
            inTag = true;
        }

        //Slow effect
        if (character == '|' && !finishedSentence)
        {
            if (textSpeed == slowSpeed)
            {
                textSpeed = normalSpeed;
            }
            else
            {
                textSpeed = slowSpeed;
            }
        }

        if (characterIndex > 0 && sentence[characterIndex - 1] == '>')
        {
            inTag = false;
        }
    }

    //Coroutine for typing sentence
    IEnumerator TypeSentence(string sentence)
    {
        //Make sure it starts from the beginning and that the text field is empty
        inTag = false;
        index = 0;
        animatorText.text = "";

        //Check for tags, and write letter in the text field
        foreach (char letter in sentence.ToCharArray())
        {
            CheckForTags(sentence, letter, index);
            animatorText.text += letter;

            ///animatorText.UpdateText(); NOT SURE TO KEEP THIS LINE OR NOT. IF THERE IS BUGS WITH TAGS, ADD THIS BACK IN

            //New voice SFX clip from current mood-pool
            voiceSource.clip = activeVoicePool[Random.Range(0, activeVoicePool.Count)];

            if (inTag)
            {
                yield return new WaitForEndOfFrame();
            }
            else
            {
                //To not overload audiosource with new clips every frame, it plays only when a clip has finished playing
                if (!voiceSource.isPlaying)
                {
                    voiceSource.Play();
                }
                yield return new WaitForSeconds(textSpeed);
            }

            index++;
        }

        //If last sentence is next, change continue to end
        if (sentences.Count == 0) { switch (currentPlatform) { case Platform.computer: continueButtonText.text = "Click to end"; break; case Platform.mobile: continueButtonText.text = "Tap to end"; break; } }
        continueButtonText.GetComponent<Animator>().Play("Text_In");
        finishedSentence = true;
    }

    void EndDialogue()
    {
        //DialogueUI out, reset text fields etc.
        StopAllCoroutines();
        if (currentPlatform == Platform.mobile) VibrationMethods.ShortLowVibration();
        anim.SetTrigger("Out");
        currentDialogue = null;
        animatorText.text = " ";
        portraitAnimator.SetTrigger("Out");
        Destroy(portraitClone, anim.GetCurrentAnimatorStateInfo(0).length);
        inDialogue = false;

        //If trigger has an event after the dialogue is finished, invoke this
        trigger.finishedDialogueEvents.Invoke();
    }
}
