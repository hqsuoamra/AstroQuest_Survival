using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyComponent))]
public class MyComponentEditor : Editor
{
    SerializedProperty myProperty;

    private void OnEnable()
    {
        myProperty = serializedObject.FindProperty("myProperty");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (myProperty != null)
        {
            EditorGUILayout.PropertyField(myProperty);
        }
        else
        {
            EditorGUILayout.HelpBox("MyProperty is not assigned.", MessageType.Warning);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
