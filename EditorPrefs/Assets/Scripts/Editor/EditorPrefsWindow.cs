using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class EditorPrefsWindow : EditorWindow
{
    [MenuItem("Tools/EditorPrefs Window")]
    private static void Init()
    {
        GetWindow<EditorPrefsWindow>().Show();
    }

    private enum EditorKey
    {
        TestBool,
        TestInt,
        TestFloat,
        TestString
    }

    private bool m_TestBool;
    private int m_TestInt;
    private float m_TestFloat;
    private string m_TestString;

    private void OnGUI()
    {
        string[] keys = System.Enum.GetNames(typeof(EditorKey));
        for(int i = 0; i < keys.Length; i++)
        {
            EditorGUI.BeginDisabledGroup(!EditorPrefs.HasKey(keys[i]));

            if(GUILayout.Button(keys[i]))
            {
                EditorPrefs.DeleteKey(keys[i]);
            }
            EditorGUI.EndDisabledGroup();
        }

        EditorGUI.BeginChangeCheck();

        m_TestBool = EditorPrefs.GetBool(EditorKey.TestBool.ToString(), true);
        m_TestBool = EditorGUILayout.Toggle("Test Bool", m_TestBool);
        //EditorPrefs.SetBool(EditorKey.TestBool.ToString(), m_TestBool);
        if (EditorGUI.EndChangeCheck())
        {
            EditorPrefs.SetBool(EditorKey.TestBool.ToString(), m_TestBool);
            // SavePrefs();
        }

        m_TestInt = EditorPrefs.GetInt(EditorKey.TestInt.ToString(), 2);
        m_TestInt = EditorGUILayout.IntField("Test Bool", m_TestInt);
        //EditorPrefs.SetInt(EditorKey.TestInt.ToString(), m_TestInt);
        if (EditorGUI.EndChangeCheck())
        {
            EditorPrefs.SetInt(EditorKey.TestInt.ToString(), m_TestInt);
            // SavePrefs();
        }

        m_TestFloat = EditorPrefs.GetFloat(EditorKey.TestFloat.ToString(), 3.4f);
        m_TestFloat = EditorGUILayout.FloatField("Test Bool", m_TestFloat);
        
        if (EditorGUI.EndChangeCheck())
        {
            EditorPrefs.SetFloat(EditorKey.TestFloat.ToString(), m_TestFloat);
            // SavePrefs();
        }

        m_TestString = EditorPrefs.GetString(EditorKey.TestString.ToString(), "hello");
        m_TestString = EditorGUILayout.TextField("Test Bool", m_TestString);
        
        if (EditorGUI.EndChangeCheck())
        {
            EditorPrefs.SetString(EditorKey.TestString.ToString(), m_TestString);
            // SavePrefs();
        }


    }

    private void SavePrefs()
    {
        // EditorPrefs.SetBool(EditorKey.TestBool.ToString(), m_TestBool);
        // EditorPrefs.SetInt(EditorKey.TestInt.ToString(), m_TestInt);
        // EditorPrefs.SetFloat(EditorKey.TestFloat.ToString(), m_TestFloat);
        // EditorPrefs.SetString(EditorKey.TestString.ToString(), m_TestString);
    }
}
