using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Contains all system event calls
/// </summary>
public static class TriggerDialogueHandler 
{
    /// <summary>
    /// Prompts the dialogue UI controller to activate and 
    /// begin the dialogue conversation
    /// </summary>
    public static event Action<ConversationData> OnTriggerConversation;
    public static void TriggerConversation(ConversationData ConvoToTrigger) => OnTriggerConversation?.Invoke(ConvoToTrigger);


    /// <summary>
    /// Prompts the dialogue UI controller continue the
    /// conversation if able
    /// </summary>
    public static event Action OnAdvanceConversation;
    public static void AdvanceConversation() => OnAdvanceConversation?.Invoke();


    /// <summary>
    /// Prompts the dialogue UI controller to activate and 
    /// begin the dialogue conversation
    /// </summary>
    public static event Action OnEndConversation;
    public static void EndConversation() => OnEndConversation?.Invoke();
    
}
