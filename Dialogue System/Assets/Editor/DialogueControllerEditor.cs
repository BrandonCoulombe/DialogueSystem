using System;
using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
using System.Dynamic;

[CustomEditor(typeof(DialogueController))]
public class DialogueControllerEditor : Editor
{
    //Type writer effect fields
    [SerializeField] SerializedProperty sp_dialogueEvents;
    [SerializeField] SerializedProperty sp_activeDialogueSequence;
    private void OnEnable()
    {
        sp_dialogueEvents = serializedObject.FindProperty("dialogueEvents");
        sp_activeDialogueSequence = serializedObject.FindProperty("activeDialogueSequence");
    }
    public override void OnInspectorGUI()
	{
        serializedObject.Update();
        
        DialogueController myScript = (DialogueController)target;

        //Active diaglouge
        EditorGUILayout.PropertyField(sp_activeDialogueSequence, new GUIContent("Active Dialogue"), GUILayout.Height(20));

        // Whether we want to show events on the behavior
        myScript.useEvents = EditorGUILayout.Toggle("Show Events", myScript.useEvents);

        if (myScript.useEvents)
        {
            // Default size w/ 0-1 event listener
            var eventAreaHeight = 225;
            var eventAreaWidth = 400;

            // Get total # of events in our - TODO use the list
            int totalEventsExtended = GetEventsExtended(myScript);

            // Set the height shift based on the # of events
            var heightChange = 50 * totalEventsExtended;

            // Change the event area height only if we have more then 1 event
            if (totalEventsExtended > 0) {
                eventAreaHeight += heightChange;
            }

            //Fields for events
            EditorGUILayout.PropertyField(sp_dialogueEvents, new GUIContent("Dialogue Events"), GUILayout.Height(eventAreaHeight), GUILayout.Width(eventAreaWidth));
        }

        // Apply changes
        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Returns the number of events that the inspector had to make room for
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public int GetEventsExtended(DialogueController target)
    {
        var a = target.dialogueEvents.OnBeginDialogueSequence.GetPersistentEventCount();
        var b = target.dialogueEvents.OnEndDialogueSequence.GetPersistentEventCount();
        var totalEvents = a + b;

        if(totalEvents <= 1) { return 0; }
        if (totalEvents == 2 && (a == 2 || b == 2))
        {
            return 1;
        }
        else 
        {
            a -= 1; // Account for first event not needing extra space
            b -= 1; // Account for first event not needing extra space
            var r = Mathf.Clamp(a, 0, 50) + Mathf.Clamp(b, 0, 50);
            return r;
        }
    }
}
