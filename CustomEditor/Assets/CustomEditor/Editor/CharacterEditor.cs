using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{
    private const char UP_ARROW = (char)9650;
    private const char DOWN_ARROW = (char)9660;

    private bool m_ListFoldout;
    private Character m_Character;

    private void OnEnable()
    {
        m_Character = (Character)target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Progress Bar"))
        {
            int count = 1000;
            for (int i = 0; i < count; i++)
            {
                Debug.Log(i);
                float progress = i / (float)count;
                if (EditorUtility.DisplayCancelableProgressBar("Log", "Logging... " + i.ToString() + " / " + count.ToString(), progress))
                {
                    //EditorUtility.DisplayProgressBar("Log", "Logging... " + i.ToString() + " / " + count.ToString(), progress);

                    break;
                }
            }

            EditorUtility.ClearProgressBar();
        }

        m_Character.m_Health = EditorGUILayout.Slider("Health", m_Character.m_Health, 0f, 100f);

        // 2e methode
        SerializedProperty nameProp = serializedObject.FindProperty("m_Name");
        EditorGUILayout.PropertyField(nameProp);

        SerializedProperty tiers = serializedObject.FindProperty("m_Tiers");
        EditorGUILayout.PropertyField(tiers, true);


        // 3eme
        ShowCustomList(tiers);

        serializedObject.ApplyModifiedProperties();

        //DrawDefaultInspector();
    }

    private void ShowCustomList(SerializedProperty i_Prop)
    {
        EditorGUILayout.BeginHorizontal();
        m_ListFoldout = EditorGUILayout.Foldout(m_ListFoldout, "Tiers", true);

        GUI.color = Color.green;
        if (GUILayout.Button("+", GUILayout.Width(20f)))
        {
            i_Prop.arraySize++;
        }
        GUI.color = Color.magenta;
        if (GUILayout.Button("x", GUILayout.Width(20f)))
        {
            i_Prop.arraySize = 0;
        }
        GUI.color = Color.white;
        EditorGUILayout.EndHorizontal();

        if (m_ListFoldout)
        {
            EditorGUI.indentLevel++;
            // i_Prop.arraySize = EditorGUILayout.IntField("Size", i_Prop.arraySize); // Version Raph Style
            EditorGUILayout.PropertyField(i_Prop.FindPropertyRelative("Array.size"));
            for (int i = 0; i < i_Prop.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal(); // On peut ajouter GUI.skin.box entre les parenthèse for look
                EditorGUILayout.PropertyField(i_Prop.GetArrayElementAtIndex(i));

                GUI.color = Color.cyan;
                if (GUILayout.Button(UP_ARROW.ToString(), EditorStyles.toolbarButton, GUILayout.Width(20f)))
                {
                    i_Prop.MoveArrayElement(i, i - 1);
                }
                GUI.color = Color.cyan;
                if (GUILayout.Button(DOWN_ARROW.ToString(), EditorStyles.toolbarButton, GUILayout.Width(20f)))
                {
                    i_Prop.MoveArrayElement(i, i + 1);
                }

                GUI.color = Color.red;
                if (GUILayout.Button("-", GUILayout.Width(20f)))
                {
                    i_Prop.DeleteArrayElementAtIndex(i);
                    i--;
                }
                GUI.color = Color.white;
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;
        }
    }
}
