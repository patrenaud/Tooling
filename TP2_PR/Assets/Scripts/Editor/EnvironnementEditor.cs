using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Environnement))]
public class EnvironnementEditor : Editor
{
    private Environnement m_Environnement;

    private float m_MinOffset;
    private float m_MaxOffset;
    private float m_RandomOffset;

    private int m_MinRotation;
    private int m_MaxRotation;
    private int m_RandomRotation;

    private float m_MinScale;
    private float m_MaxScale;
    private float m_RandomScale;

    private void OnEnable()
    {
        m_Environnement = (Environnement)target;
    }

    public override void OnInspectorGUI()
    {
        ShowOffset();
        ShowRotation();
        ShowScale();
        ShowOther();
        
        DrawPropertiesExcluding(serializedObject, "m_Health");

        serializedObject.ApplyModifiedProperties(); // Accepte que des changements peuvent se faire dans l'éditeur
    }

    private void ShowOffset()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Random Position Offset", EditorStyles.centeredGreyMiniLabel);
        m_MinOffset = EditorGUILayout.FloatField("Min. Offset", m_MinOffset);
        m_MaxOffset = EditorGUILayout.FloatField("Max. Offset", m_MaxOffset);

        GUI.color = Color.cyan;
        GUI.contentColor = Color.white;
        if (GUILayout.Button("Random Offset"))
        {
            m_RandomOffset = Random.Range(m_MinOffset, m_MaxOffset);
            m_Environnement.transform.position += new Vector3(m_RandomOffset, m_RandomOffset, m_RandomOffset);
        }
        GUI.color = Color.white;
        EditorGUILayout.EndVertical();
    }

    private void ShowRotation()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Random Rotation", EditorStyles.centeredGreyMiniLabel);
        m_MinRotation = EditorGUILayout.IntField("Min. Rotation", m_MinRotation);
        m_MaxRotation = EditorGUILayout.IntField("Max. Rotation", m_MaxRotation);

        GUI.color = Color.red;
        GUI.contentColor = Color.white;
        if (GUILayout.Button("Random Rotation"))
        { 
            m_RandomRotation = Random.Range(m_MinRotation, m_MaxRotation);
            m_Environnement.transform.rotation = Quaternion.Euler(m_RandomRotation, m_RandomRotation, m_RandomRotation);
        }
        GUI.color = Color.white;
        EditorGUILayout.EndVertical();
    }

    private void ShowScale()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Random Scale", EditorStyles.centeredGreyMiniLabel);
        m_MinScale = EditorGUILayout.FloatField("Min. Scale", m_MinScale);
        m_MaxScale = EditorGUILayout.FloatField("Max. Scale", m_MaxScale);

        GUI.color = Color.yellow;
        GUI.contentColor = Color.white;
        if (GUILayout.Button("Random Scale"))
        {
            m_RandomScale = Random.Range(m_MinScale, m_MaxScale);
            m_Environnement.transform.localScale = new Vector3(m_RandomScale, m_RandomScale, m_RandomScale);
        }
        
        GUI.color = Color.white;
        EditorGUILayout.EndVertical();
    }

    private void ShowOther()
    {
        SerializedProperty health = serializedObject.FindProperty("m_Health");
        health.intValue = EditorGUILayout.IntSlider("Health", health.intValue, 0, 100);
    }

}
