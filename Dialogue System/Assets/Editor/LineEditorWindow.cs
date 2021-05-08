using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

//[CustomEditor(typeof(Line))]
class LineEditorWindow : Editor
{
  public override VisualElement CreateInspectorGUI()
  {
    var container = new VisualElement();
    //UIElementsEditorHelper.FillDefaultInspector(container, serializedObject, false);
    return container;
  }
}