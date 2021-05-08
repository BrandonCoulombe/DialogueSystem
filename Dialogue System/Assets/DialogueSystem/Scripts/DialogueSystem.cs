using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField]
    public ConversationData ActiveDialogueSequence
    {
        get => activeDialogueSequence;
        set => activeDialogueSequence = value;
    }
    public ConversationData activeDialogueSequence;
    public DialogueCallbacks dialogueEvents;
    public bool useEvents;
    protected SpeakerUI speakerUI;
    protected int activeLineIndex = 0;
}

/// <summary>
///  A serializable class to display all dialogue events
///  You may add and invoke new events as you please
/// </summary>
[System.Serializable]
public class DialogueCallbacks
{
    [HideInInspector] public List<UnityEvent> eventList = new List<UnityEvent>();
    public UnityEvent OnBeginDialogueSequence;
    public UnityEvent OnEndDialogueSequence;

    DialogueCallbacks()
    {
        eventList.Add(OnBeginDialogueSequence);
        eventList.Add(OnEndDialogueSequence);
    }
}

enum DialgoueControlType
{
    Automatic,
    Click,
    ButtonChoice,
}
