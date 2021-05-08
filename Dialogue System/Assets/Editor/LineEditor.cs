using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Security.Policy;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class UIElementsDrawerType
{
	public DialogueType DialogueType;
	public Character Character;
	public bool SpokeLast;
	public string Text;
}

[CanEditMultipleObjects]
[CustomPropertyDrawer(typeof(Line))]
public class LineEditor : PropertyDrawer
{
	private DialogueType dialogueType;
	private float totalHeight = 0;
	private bool shouldUpdateHeight = true;
	public Character character;

	[TextArea(2, 6)]
	public string DialogueText;

	/*
	public override VisualElement CreatePropertyGUI(SerializedProperty property)
	{
		var container = new VisualElement();

		if (property.serializedObject == null)
		{
			return container;
		}

		SerializedObject so = new SerializedObject(property.objectReferenceValue);
		Debug.Log(so.targetObject.ToString());	
		var iterator = so.GetIterator();
		if (iterator.NextVisible(true))
		{
			do
			{
				var propertyField = new PropertyField(iterator);

				if (iterator.propertyPath == "DialogueType")
				{
					Debug.Log("asdas");
					propertyField.SetEnabled(value: false);
				}

				container.Add(propertyField);
			}
			while (iterator.NextVisible(false));
		}

		//We bind to the new SerializedObject
		container.Bind(so);
		return container;
	}
	*/

	

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		label = EditorGUI.BeginProperty(position, label, property);
		//EditorGUI.BeginChangeCheck();


		//CreatePropertyGUI(property.FindPropertyRelative("DialogueType"));

		//position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), GUIContent.none);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 5;

		// Always shown data
		var typeRectHeight = 20;
		var typeRect = new Rect(position.x, position.y + 5, position.width, typeRectHeight);
		var typeNameRect = new Rect(typeRect.x - 80, typeRect.y, typeRect.width, typeRectHeight);
		dialogueType = (DialogueType)EditorGUI.EnumPopup(typeRect, dialogueType);
		EditorGUI.LabelField(typeNameRect, new GUIContent { text = "Action Type" });

		// Case data
		EditorGUI.indentLevel = indent;
		switch (dialogueType)
		{
			case DialogueType.Choices:
				UpdatePropertyHeight(typeRectHeight + typeRectHeight);
				break;
			case DialogueType.Regular:
				var charRectHeight = 20;
				var characterRect = new Rect(position.x, position.y + 25, position.width, charRectHeight);
				var textRectHeight = 40;
				var textRect = new Rect(position.x, position.y + 50, position.width, textRectHeight);


				EditorGUI.PropertyField(characterRect, property.FindPropertyRelative("Character"));
				DialogueText = (string)EditorGUI.TextArea(textRect, DialogueText);
				UpdatePropertyHeight((typeRectHeight * 2) + (charRectHeight + textRectHeight));

				break;
			default:
				break;
		}




		property.serializedObject.ApplyModifiedProperties();


		EditorGUI.EndChangeCheck();
		EditorGUI.EndProperty();
	}

	public void UpdatePropertyHeight(float heightToAdd)
	{
		shouldUpdateHeight = false;
		totalHeight = heightToAdd;
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return base.GetPropertyHeight(property, label) + totalHeight;
	}
}

/*
public static class UIElementsEditorHelper
{
	public static void FillDefaultInspector(VisualElement container, SerializedObject serializedObject, bool hideScript)
	{
		SerializedProperty property = serializedObject.GetIterator();
		if (property.NextVisible(true)) // Expand first child.
		{
			do
			{
				if (property.propertyPath == "m_Script" && hideScript)
				{
					continue;
				}
				var field = new PropertyField(property);
				field.name = "PropertyField:" + property.propertyPath;


				if (property.propertyPath == "m_Script" && serializedObject.targetObject != null)
				{
					field.SetEnabled(false);
				}

				container.Add(field);
			}
			while (property.NextVisible(false));
		}
	}
}*/