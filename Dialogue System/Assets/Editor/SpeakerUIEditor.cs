using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpeakerUI))]
public class SpeakerUIEditor : Editor
{
    //Type writer effect fields
    SerializedProperty sp_backgroundImage;
    SerializedProperty sp_portraitImage;
    SerializedProperty sp_nameText;
    SerializedProperty sp_dialogueText;

    private void OnEnable()
    {
        sp_backgroundImage = serializedObject.FindProperty("Background");
        sp_portraitImage = serializedObject.FindProperty("PortraitImage");
        sp_nameText = serializedObject.FindProperty("NameText");
        sp_dialogueText = serializedObject.FindProperty("DialogueText");
    }

    public override void OnInspectorGUI()
	{
        
        var myScript = target as SpeakerUI;

        // Type writer effect fields
        GUILayout.BeginHorizontal();
            myScript.TypewriterEffect = EditorGUILayout.Toggle("Typewriter Effect : Delay", myScript.TypewriterEffect);
            using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(!myScript.TypewriterEffect)))
            {
                if (group.visible == false)
                {  
                    myScript.TypewriterDelay = EditorGUILayout.FloatField(myScript.TypewriterDelay);
                }
            }
        GUILayout.EndHorizontal();

        //Fields for public object references
        EditorGUI.BeginDisabledGroup(true);
           EditorGUILayout.PropertyField(sp_backgroundImage, new GUIContent("Backgroud Object"), GUILayout.Height(20));
           EditorGUILayout.PropertyField(sp_portraitImage, new GUIContent("Portrait Object"), GUILayout.Height(20));
           EditorGUILayout.PropertyField(sp_nameText, new GUIContent("Name Object"), GUILayout.Height(20));
           EditorGUILayout.PropertyField(sp_dialogueText, new GUIContent("Dialogue Object"), GUILayout.Height(20));
        EditorGUI.EndDisabledGroup();

    }
}
