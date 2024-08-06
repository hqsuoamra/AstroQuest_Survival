using UnityEditor;
using UnityEngine;

public class FixFBXFramerate : MonoBehaviour
{
    [MenuItem("Tools/Fix FBX Framerate")]
    private static void FixFramerate()
    {
        string[] fbxFiles = AssetDatabase.FindAssets("t:Model", new[] { "Assets" });

        foreach (string guid in fbxFiles)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;

            if (importer != null)
            {
                importer.animationCompression = ModelImporterAnimationCompression.Off;
                importer.animationPositionError = 0.5f;
                importer.animationRotationError = 0.5f;
                importer.animationScaleError = 0.5f;

                // Fix the framerate
                SerializedObject serializedObject = new SerializedObject(importer);
                SerializedProperty clipAnimations = serializedObject.FindProperty("m_ClipAnimations");

                if (clipAnimations != null && clipAnimations.isArray)
                {
                    for (int i = 0; i < clipAnimations.arraySize; i++)
                    {
                        SerializedProperty clip = clipAnimations.GetArrayElementAtIndex(i);
                        SerializedProperty framerate = clip.FindPropertyRelative("m_SampleRate");

                        if (framerate != null && framerate.floatValue == 0f)
                        {
                            framerate.floatValue = 24f; // Set to a default value
                        }
                    }
                }

                serializedObject.ApplyModifiedProperties();
                AssetDatabase.ImportAsset(path);
            }
        }

        Debug.Log("FBX framerate fix completed.");
    }
}
