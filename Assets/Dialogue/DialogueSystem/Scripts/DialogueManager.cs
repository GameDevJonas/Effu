using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private PlayerInputs inputs;

    public enum Platform { computer, mobile };
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
    private Queue<string> names;
    private Queue<string> sentences;
    private Queue<AudioClip> voiceLines;

    [Header("UI variables")]
    public Animator anim;
    public TextMeshProUGUI nameText;

    [Header("Script for using tags in dialogue (Sits on 'Dialogue' child of UI)")]
    public TextMeshAnimator animatorText;

    //Bools to check for tags, each letter in a sentence, text-skip etc.
    [HideInInspector] public bool inTag = false;
    [HideInInspector] public bool finishedSentence = false;
    private string currentSentence;
    private int index;

    [Header("Audio sources for SFX")]
    public AudioSource voiceSource;

    //Which trigger triggered the dialogue
    private DialogueTrigger trigger;

    private void Awake()
    {
        names = new Queue<string>();
        sentences = new Queue<string>();
        voiceLines = new Queue<AudioClip>();
        textSpeed = normalSpeed;
        inputs = FindObjectOfType<PlayerInputs>();
    }

    private void Start()
    {
        inputs.playerControls.Land.Jump.performed += _ => NextSentence();
    }

    //Starts dialogue
    public void StartDialogue(Dialogue dialogue, DialogueTrigger dTrigger)
    {
        if (currentPlatform == Platform.mobile) VibrationMethods.ShortLowVibration();
        inputs.DisableEnableAll(false);
        inDialogue = true;
        trigger = dTrigger;

        //DialogueUI in
        anim.SetTrigger("In");

        animatorText.BeginAnimation();

        //Set UI variables according to Dialogue ScriptableObject
        //nameText.text = dialogue.character.characterName;
        //switch (currentPlatform) { case Platform.computer: continueButtonText.text = "Click to continue"; break; case Platform.mobile: continueButtonText.text = "Tap to continue"; break; }

        //Clear all text, sentences and moods from queues
        animatorText.text = "";
        names.Clear();
        sentences.Clear();
        voiceLines.Clear();

        //Queue new dialogue/mood
        foreach (SentenceElements sentenceElement in dialogue.dialogue)
        {
            names.Enqueue(sentenceElement.name);
            sentences.Enqueue(sentenceElement.sentence);
            voiceLines.Enqueue(sentenceElement.voiceLine);
        }

        //Display next sentence
        DisplayNextSentence(false);
    }



    //Prepares next sentence
    public void DisplayNextSentence(bool fromButton)
    {
        if(!finishedSentence) voiceSource.Stop();
        //Checks if this is calles from the game or a script
        if (fromButton)
        {
            if (currentPlatform == Platform.mobile) VibrationMethods.ShortLowVibration();
            //continueButtonText.GetComponent<Animator>().Play("Text_Out");
        }
        if (!finishedSentence && fromButton)
        {
            //Skips waiting time for text to appear
            StopAllCoroutines();
            animatorText.text = currentSentence;

            //continueButtonText.GetComponent<Animator>().Play("Text_In");
            finishedSentence = true;
            return;
        }

        //Reset bool and text speed when new sentence is displayes
        finishedSentence = false;
        textSpeed = normalSpeed;

        //If more sentences are left continue, else end dialogue
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //Write next sentence
        Debug.Log(names.Peek());
        nameText.text = names.Dequeue() + ":";
        currentSentence = sentences.Dequeue();
        voiceSource.clip = voiceLines.Dequeue();
        voiceSource.Play();
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

            if (inTag)
            {
                yield return new WaitForEndOfFrame();
            }
            else
            {
                yield return new WaitForSeconds(textSpeed);
            }

            index++;
        }

        //If last sentence is next, change continue to end
        //if (sentences.Count == 0) { switch (currentPlatform) { case Platform.computer: continueButtonText.text = "Click to end"; break; case Platform.mobile: continueButtonText.text = "Tap to end"; break; } }
        //continueButtonText.GetComponent<Animator>().Play("Text_In");
        finishedSentence = true;
    }

    void EndDialogue()
    {
        //DialogueUI out, reset text fields etc.
        StopAllCoroutines();
        if (currentPlatform == Platform.mobile) VibrationMethods.ShortLowVibration();
        anim.SetTrigger("Out");
        animatorText.text = " ";
        inDialogue = false;
        //If trigger has an event after the dialogue is finished, invoke this
        trigger.finishedDialogueEvents.Invoke();
        Invoke("EnableInputs", .2f);
    }

    private void EnableInputs()
    {
        inputs.DisableEnableAll(true);
    }

    [ContextMenu("Next Sentence")]
    public void NextSentence()
    {
        if(inDialogue) DisplayNextSentence(true);
    }
}
