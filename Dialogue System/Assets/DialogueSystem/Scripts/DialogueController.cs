using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;



/// <summary>
///  Trigger dialogue sequences using the <see cref="TriggerDialogueHandler"/> class 
/// </summary>

[RequireComponent(typeof(CanvasRenderer), typeof(RectTransform))]
public class DialogueController : DialogueSystem
{ 
    /// <summary>
    /// Returns whether the conversation sequence is on its last line of dialogue and is about to close
    /// </summary>
    /// <returns></returns>
    public bool IsLastLine => activeLineIndex == activeDialogueSequence.lines.Length;

    /// <summary>
    /// Returns whether the active speaking character is going to be talking in the next line
    /// </summary>
    /// <returns></returns>
    public bool WillSpeakAgain
    {
        get
        {
            if (!IsLastLine) return activeDialogueSequence.lines[activeLineIndex - 1].Character.FullName == activeDialogueSequence.lines[activeLineIndex].Character.FullName;
            else return false;
        }
    }

    /// <summary>
    /// Returns the character that is currently speaking
    /// </summary>
    public Character ActiveSpeaker => ActiveDialogueSequence.lines[activeLineIndex].Character;

    #region Init & Event Subs
    private void Awake()
    {    
        speakerUI = transform.GetChild(0).GetComponent<SpeakerUI>();
    }
    private void OnEnable()
    {
        //Unsub
        TriggerDialogueHandler.OnTriggerConversation -= OnTriggerConvorsation;
        TriggerDialogueHandler.OnAdvanceConversation -= StartConversation;
        TriggerDialogueHandler.OnEndConversation -= EndConversation;
        //Sub
        TriggerDialogueHandler.OnTriggerConversation += OnTriggerConvorsation;
        TriggerDialogueHandler.OnAdvanceConversation += StartConversation;
        TriggerDialogueHandler.OnEndConversation += EndConversation;
    }
    private void OnDestory()
    {
        //Unsub
        TriggerDialogueHandler.OnTriggerConversation -= OnTriggerConvorsation;
        TriggerDialogueHandler.OnAdvanceConversation -= StartConversation;
        TriggerDialogueHandler.OnEndConversation -= EndConversation;
    }

    #endregion


    /// <summary>
    /// Listens to the ontriggerconversation event and starts a 
    /// new dialogue sequence
    /// </summary>
    /// <param name="m_conversation"></param>
    private void OnTriggerConvorsation(ConversationData m_conversation)
    {
        activeDialogueSequence = m_conversation;
        StartConversation();
    }

    /// <summary>
    /// Debug button in the scene view to manually test event calls
    /// and conversations
    /// </summary>
    /// <param name="convo"></param>
    public void Debug_Button(ConversationData convo)
    {
        TriggerDialogueHandler.TriggerConversation(convo);
    }

    /// <summary>
    /// Beings the conversation coroutine
    /// </summary>
    public void StartConversation() => StartCoroutine(ConversationRoutine());

    /// <summary>
    /// Called when the dialogue is advanced and there are no more lines left
    /// </summary>
    private void EndConversation()
    {
        speakerUI.Hide();
        activeLineIndex = 0;
        activeDialogueSequence = null;
        dialogueEvents.OnEndDialogueSequence?.Invoke();
    }

    /// <summary>
    /// Main dialogue method - Advances the conversation to the next line
    /// and also checks if its the first or last line of the conversation
    /// to call events and broadcast messages
    /// </summary>
    public IEnumerator ConversationRoutine()
    {
        if (activeDialogueSequence == null)
        {
            StopAllCoroutines();
        }

        //If there are still lines left
        if (activeLineIndex < activeDialogueSequence.lines.Length)
        {
            // Start of dialogue sequence
            if (activeLineIndex == 0)
            {
                dialogueEvents.OnBeginDialogueSequence.Invoke();
            }

            DisplayLine();  //This is when we enable UI n stuff
            activeLineIndex++; //move to next sentence       
        }
        else // End of dialogue sequence
        {
            EndConversation();
        }
        yield return null;
    }

    /// <summary>
    /// Handles displaying a line
    /// </summary>
    private void DisplayLine()
    {
        Line line = activeDialogueSequence.lines[activeLineIndex];

        if (line.SpokeLast)
        {
            ContinueDialogue(speakerUI, line.Text);
        }
        else
        {
            SetNewSpeaker(speakerUI,
              line.Text,
              line.Character.FullName,
              line.Character.Portrait);
        }
    }

    /// <summary>
    /// Updates only the text
    /// </summary>
    /// <param name="activeSpeaker"></param>
    /// <param name="text"></param>
    public void ContinueDialogue(SpeakerUI m_activeSpeaker, string m_text)
    {
        m_activeSpeaker.Dialogue = m_text;
    }

    /// <summary>
    /// When there is a new speaker, we call this function and set the 
    /// portrait, name, text, to that new speaker before we enable the UI
    /// </summary>
    /// <param name="activeSpeakerUI">Parent Obj of the Speaker Prefab</param>
    /// <param name="m_text">Text field within the speaker UI</param>
    /// <param name="m_dialogueBoxName">The Speakers Name</param>
    /// <param name="m_portrait">The Speakers IMG</param>
    private void SetNewSpeaker(
    SpeakerUI m_activeSpeakerUI,
    string m_text,
    string m_SpeakerName,
    Sprite m_portrait)
    {
        //Set variables to the new active speaker
        m_activeSpeakerUI.NameText.text = m_SpeakerName;
        m_activeSpeakerUI.Dialogue = m_text;
        m_activeSpeakerUI.PortraitImage.sprite = m_portrait;

        //show UI
        m_activeSpeakerUI.Show();
    }
}
