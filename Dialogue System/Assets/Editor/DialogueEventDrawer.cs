using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using JetBrains.Annotations;
using UnityEngine.Events;
using System.Reflection;

//[CustomPropertyDrawer(typeof(DialogueCallbacks))] //Custom class with UnityEvents
//public class DialogueEventDrawer : PropertyDrawer
//{
//    [Tooltip("Reference to the main dialogue controller")]
//    private DialogueController dialogueController;

//    [Tooltip("Holds data for the OnBeginDialogue UnityEvent")]
//    private EventFieldBucket OnBeginDialogue = null;

//    [Tooltip("Holds data for the OnEndDialogue UnityEvent")]
//    private EventFieldBucket OnEndDialogue = null;

//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        return base.GetPropertyHeight(property, label);
//    }

//    /*
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        // Start
//        EditorGUI.BeginProperty(position, label, property);
//        EditorGUI.BeginChangeCheck();


//        // Get the dialogue controller - calls only once 
//        if (dialogueController == null) 
//            dialogueController = (DialogueController)Object.FindObjectOfType(typeof(DialogueController));
        

//        // Create internal 'buckets'
//        if (OnBeginDialogue == null) {
//            OnBeginDialogue = new EventFieldBucket {
//                Name = "On Begin Dialogue",
//                CurrentPosition = new Rect(position.x + 20, position.y + 50, 300, 600),
//                DefaultPosition = new Rect(position.x + 20, position.y + 50, 300, 600),
//                
//            };
//        }

//        if (OnEndDialogue == null) {
//            OnEndDialogue = new EventFieldBucket {
//                Name = "On End Dialogue",
//                CurrentPosition = new Rect(position.x + 20, position.y + 150, 300, 600),
//                DefaultPosition = new Rect(position.x + 20, position.y + 150, 300, 600),
//                SerializedProperty = property.FindPropertyRelative("OnEndDialogueSequence")
//            };
//        }

//        // Store all of our buckets in a list - we can use this for logic
//        List<EventFieldBucket> eventFieldDrawers = new List<EventFieldBucket>();
//        eventFieldDrawers.Add(OnBeginDialogue);
//        eventFieldDrawers.Add(OnEndDialogue);

//        // Draw label
//        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), GUIContent.none);

//        // Don't make child fields be indented
//        var indent = EditorGUI.indentLevel;
//        EditorGUI.indentLevel = 0;

//        //Repositon property fields when new elements are added to a list
//        if (GUI.changed)
//        {
//            Debug.Log("Changed");
//        }
       
//        // Draw fields in order
//        for (int i = 0; i < eventFieldDrawers.Count; i++)
//        {
//            Debug.Log("Event Count: " + EventHelperMethods.GetListenerNumber(dialogueController.dialogueEvents.eventList[i]));
//            //Debug.Log("Event Count: " + dialogueController.dialogueEvents.eventList[i].GetPersistentMethodName(i));

//            // Move all buckets that are below this bucket downwards to make room for new events
//            if (dialogueController.dialogueEvents.eventList[i].GetPersistentEventCount() > 1 && (i+1) < eventFieldDrawers.Count)
//            {
//                Debug.Log("Resizing bucket: " + dialogueController.dialogueEvents.eventList[i].ToString());
//                eventFieldDrawers[i+1].ResizeBucket(dialogueController.dialogueEvents.eventList[i+1], ref eventFieldDrawers[i+1].CurrentPosition);
//            }
//            EditorGUI.PropertyField(eventFieldDrawers[i].CurrentPosition, eventFieldDrawers[i].SerializedProperty, new GUIContent { text = eventFieldDrawers[i].Name });
//        }

//        // Reset indent
//        EditorGUI.indentLevel = indent;

//        // End
//        EditorGUI.EndChangeCheck();
//        EditorGUI.EndProperty();
//    }

//    */



//    class EventFieldBucket
//    {
//        public string Name;
//        public Rect CurrentPosition;
//        public Rect DefaultPosition;
//        public SerializedProperty SerializedProperty;
//        private const int BUCKET_GROWTH_Y = 15;
//        public void ResizeBucket(UnityEvent @event, ref Rect currentPos)
//        {    
//            var newYPos = DefaultPosition.y + (Mathf.Clamp(@event.GetPersistentEventCount(), 2, 15) * BUCKET_GROWTH_Y);
//            currentPos = new Rect(DefaultPosition.x, newYPos, DefaultPosition.height, DefaultPosition.width);
//        }
//    }
//}
