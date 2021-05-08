using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[System.Serializable]
public class Line
{
  [Header("Dialogue Type")]
  public DialogueType DialogueType;

    public Character Character;

    [Header("Did they speak last as well?")]
    public bool SpokeLast;

   [TextArea(2, 6)]
    public string Text;
}

[CreateAssetMenu(fileName = "Convorsation", menuName = "Dialogue/New Conversation")][System.Serializable]
public class ConversationData : ScriptableObject
{
    [Header("Specific Convorsation Variables (Read Mouseover)")]
    [Tooltip("Plays dialogue automatically?")]
    public bool AutomaticConversation;

    [Header("Generic Convorsation Variables")] 
    public Line[] lines;
}

public enum DialogueType
{
  Regular,
  Choices,
}