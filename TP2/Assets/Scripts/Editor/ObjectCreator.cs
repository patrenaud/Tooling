using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectCreator : EditorWindow
{
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Back
    }

    private Direction m_Direction = Direction.Right;
    private PrimitiveType m_PrimitiveType;

    private Color m_StartingColor;
    private Color m_EndColor;

    private Transform m_Transform;
    private GameObject m_CustomObject;

    private List<GameObject> m_ObjectList = new List<GameObject>();

    private int m_NbToCreate;
    private int m_Spacing = 1;    

    private bool m_UseColorToggle = false;
    private bool m_AutoCenter = false;
    private bool m_UseLocalRotation = false;

    private string m_Name;
    private string m_Type;

    [MenuItem("Toolings/ObjectCreator")]
    private static void Init()
    {
        ObjectCreator script = GetWindow<ObjectCreator>();
        script.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Caliss", EditorStyles.centeredGreyMiniLabel);
        ShowPosition();
        ShowObjectSelection();
        ShowCreationParameters();
        ShowObjectCreator();
    }

    private void ShowPosition()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box); // crée une box comme style
        EditorGUILayout.LabelField("Position", EditorStyles.centeredGreyMiniLabel); // Regroupe en rectangle clean
        if (m_Transform == null)
        {
            EditorGUILayout.LabelField(Vector3.zero.ToString(), EditorStyles.centeredGreyMiniLabel);
        }
        else
        {
            EditorGUILayout.LabelField(m_Transform.transform.position.ToString(), EditorStyles.centeredGreyMiniLabel);
        }
        // Cette variable représente la position de départ de l'objet
        m_Transform = (Transform)EditorGUILayout.ObjectField("Transform: ", m_Transform, typeof(Transform), true); // **********
        EditorGUILayout.EndVertical();
    }

    private void ShowObjectSelection()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Object Selection", EditorStyles.centeredGreyMiniLabel);

        // Ce bouton permet de choisir un objet custom NOT WORKING !!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (GUILayout.Button("Select custom object"))
        {
            m_CustomObject = (GameObject)EditorGUILayout.ObjectField("Object: ", m_CustomObject, typeof(GameObject), true);
           
        }
        else
        {
            /*int index = ArrayUtility.IndexOf(m_PrimitiveTypes, m_Type);
            index = EditorGUILayout.Popup("Type", index, m_PrimitiveTypes);
            if (index != -1)
            {
                m_Type = m_PrimitiveTypes[index]; // *************
            }*/
            m_PrimitiveType = (PrimitiveType)EditorGUILayout.EnumPopup("Primitive Type: ", m_PrimitiveType);
        }
        EditorGUILayout.EndVertical();
    }

    private void ShowCreationParameters()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Creation Parameters", EditorStyles.centeredGreyMiniLabel);

        // Represente le nom de l'objet
        m_Name = EditorGUILayout.TextField("Name: ", m_Name); // ***************

        // Creation de 2 field pour la couleur de départ et de fin
        m_UseColorToggle = EditorGUILayout.Toggle("UseColor? ", m_UseColorToggle);
        if (m_UseColorToggle)
        {
            m_StartingColor = EditorGUILayout.ColorField("Starting Color: ", m_StartingColor);
            m_EndColor = EditorGUILayout.ColorField("Starting Color: ", m_EndColor);
        }
        else
        {
            m_StartingColor = Color.white;
            m_EndColor = Color.white;
        }
        // Variable pour le nombre d'objets à instanciés
        m_NbToCreate = EditorGUILayout.IntField("Nb to create: ", m_NbToCreate); // ********
        // Variable pour la distance entre les objets
        m_Spacing = EditorGUILayout.IntField("Spacing: ", m_Spacing); // *******

        m_Direction = (Direction)EditorGUILayout.EnumPopup("Axis Direction: ", m_Direction);

        m_AutoCenter = EditorGUILayout.Toggle("Auto-Center? ", m_AutoCenter);

        if (m_Transform != null)
        {
            // ERROR !!!!!!!!!!!!!!!!!!
            m_UseLocalRotation = EditorGUILayout.Toggle("Use local rotation? ", m_UseLocalRotation);
            if (m_UseLocalRotation)
            {
               // m_Object.transform.rotation = m_Transform.rotation;
            }
        }
    }

    private void ShowObjectCreator()
    {
        if (GUILayout.Button("Create Object"))
        {
            //for (int i = 0; i < m_NbToCreate; i++)
           // m_ObjectList[i] = Instantiate(m_Object, m_Transform.position * i , Quaternion.identity);
        }

    }

}
