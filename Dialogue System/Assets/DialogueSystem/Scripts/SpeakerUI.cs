using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// Holds data for the current speakers UI
/// Scripts are able to get the active speakers name
/// and dialogue as well as call basic methods that display and hide 
/// the speaker
/// </summary>

public class SpeakerUI : MonoBehaviour
{
    [Tooltip("Main Dialogue Handler")]
    private DialogueController dialogueController;

    public bool TypewriterEffect;
    public float TypewriterDelay;

    public Image Background;
    public Image PortraitImage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DialogueText;



    private Character speaker;
    public Character Speaker
    {
        get => speaker; 
        set {
            speaker = value;
            PortraitImage.sprite = speaker.Portrait;
            NameText.text = speaker.FullName;
        }
    }

    /// <summary>
    /// Hide the dialogue box at the start of game and get the dialogue controller
    /// </summary>
    private void Awake() 
    {

        dialogueController = GetComponentInParent<DialogueController>();

        Hide(); 
    }

    /// <summary>
    /// Main text field string
    /// </summary>
    public string Dialogue
    {
        set {
            if (TypewriterEffect)
            {
                StopAllCoroutines();
                StartCoroutine(TypewriterEffectSequence(value));
            }
            else
            {
                DialogueText.text = value;
            }
        }
        get => DialogueText.text;
    }


    public IEnumerator TypewriterEffectSequence(string m_text)
    {
        // Ensure the text is clear
        DialogueText.text = "";

        //Type writer effect
        foreach (char c in m_text)
        {
            DialogueText.text += c;
            yield return new WaitForSeconds(TypewriterDelay);
        }

        //If not on the last line and the character is going to speak again, we end with 3 dots
        if (!dialogueController.IsLastLine && dialogueController.WillSpeakAgain)
        {
            for (int i = 0; i < 3; i++)
            {
                DialogueText.text += ".";
                yield return new WaitForSeconds(TypewriterDelay);
            }
            //Add and remove the last "." in the line to incentivise that there is more to say
            while (true) 
            {
                yield return new WaitForSeconds(0.5f);
                DialogueText.text = DialogueText.text.Remove(DialogueText.text.Length - 1);
                yield return new WaitForSeconds(0.5f);
                DialogueText.text += ".";
            }
        }
        yield return null;
    }

    /// <summary>
    /// Checks if there is an active speaker
    /// </summary>
    /// <returns></returns>
    public bool HasSpeaker => Speaker != null;
     

    /// <summary>
    /// Enables all dialogue elements
    /// </summary>
    public void Show()
    {
        Background.enabled = true;
        PortraitImage.enabled = true;
        NameText.enabled = true;
        DialogueText.enabled = true;
    }

    /// <summary>
    /// Hides all dialogue elements
    /// </summary>
    public void Hide()
    {
        PortraitImage.enabled = false;
        NameText.enabled = false;
        DialogueText.enabled = false;
        Background.enabled = false;
    }
}
  
