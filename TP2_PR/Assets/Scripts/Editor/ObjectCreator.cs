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

    private Vector3 m_StartingPos = new Vector3(0f, 0f, 0f);

    private bool m_UseColorToggle = false;
    private bool m_AutoCenter = false;
    private bool m_UseLocalRotation = false;

    private string m_Name;

    [MenuItem("Toolings/ObjectCreator")]
    private static void Init()
    {
        ObjectCreator script = GetWindow<ObjectCreator>();
        script.Show();
    }

    private void OnGUI()
    {
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
            GUI.color = Color.yellow;
            EditorGUILayout.HelpBox("If no Pos is assigned the Pos = 0,0,0", MessageType.Warning);
            GUI.color = Color.white;
        }
        else
        {
            EditorGUILayout.LabelField(m_Transform.transform.position.ToString(), EditorStyles.centeredGreyMiniLabel);
        }
        // Cette variable représente la position de départ de l'objet
        m_Transform = (Transform)EditorGUILayout.ObjectField("Transform: ", m_Transform, typeof(Transform), true);
        EditorGUILayout.EndVertical();
    }

    private void ShowObjectSelection()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.LabelField("Object Selection", EditorStyles.centeredGreyMiniLabel);

        m_CustomObject = (GameObject)EditorGUILayout.ObjectField("Custom Object: ", m_CustomObject, typeof(GameObject), true);

        m_PrimitiveType = (PrimitiveType)EditorGUILayout.EnumPopup("Primitive Type: ", m_PrimitiveType);

        EditorGUILayout.EndVertical();
    }

    private void ShowCreationParameters()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Creation Parameters", EditorStyles.centeredGreyMiniLabel);

        // Represente le nom de l'objet
        m_Name = EditorGUILayout.TextField("Name: ", m_Name);
        if (m_Name == "")
        {
            m_Name = "Chosse something plz";
        }

        // Creation de 2 field pour la couleur de départ et de fin
        m_UseColorToggle = EditorGUILayout.Toggle("UseColor? ", m_UseColorToggle);
        if (m_UseColorToggle)
        {
            m_StartingColor = EditorGUILayout.ColorField("Starting Color: ", m_StartingColor);
            m_EndColor = EditorGUILayout.ColorField("End Color: ", m_EndColor);
        }
        else
        {
            m_StartingColor = Color.white;
            m_EndColor = Color.white;
        }

        // Variable pour le nombre d'objets à instanciés
        m_NbToCreate = EditorGUILayout.IntField("Nb to create: ", m_NbToCreate);

        // Variable pour la distance entre les objets
        m_Spacing = EditorGUILayout.IntField("Spacing: ", m_Spacing);


        m_Direction = (Direction)EditorGUILayout.EnumPopup("Axis Direction: ", m_Direction);
        GetDirection();

        m_AutoCenter = EditorGUILayout.Toggle("Auto-Center? ", m_AutoCenter); // TO DO

        if (m_Transform)
        {
            m_UseLocalRotation = EditorGUILayout.Toggle("Use local rotation? ", m_UseLocalRotation);
        }
        EditorGUILayout.EndVertical();
    }

    private void ShowObjectCreator()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        if (GUILayout.Button("Create Object"))
        {
            GameObject parent = new GameObject(); // Here is the parent for all gameObjects created
            Undo.RegisterCreatedObjectUndo(parent, "Parent"); // To add the ctrl-Z feature
            m_ObjectList.Clear();
            for (int i = 0; i < m_NbToCreate; i++)
            {
                if (m_CustomObject == null)
                {
                    m_ObjectList.Add(GameObject.CreatePrimitive(m_PrimitiveType));
                    m_ObjectList[i].transform.SetParent(parent.transform); // This is to add in parent

                    if (m_Transform == null)
                    {
                        if (m_UseLocalRotation)
                        {
                            m_ObjectList[i].transform.position = m_StartingPos + i * GetDirection() * m_Spacing;
                        }
                        else
                        {
                            m_ObjectList[i].transform.position = m_StartingPos + i * GetDirection() * m_Spacing;
                        }
                    }
                    else
                    {
                        if (m_UseLocalRotation)
                        {
                            m_ObjectList[i].transform.position = m_StartingPos + i * GetDirection() * m_Spacing;
                        }
                        else
                        {
                            m_ObjectList[i].transform.position = m_Transform.position + i * GetDirection() * m_Spacing;
                        }
                    }
                }

                else // With CustomObject
                {
                    if (m_Transform == null)
                    {
                        m_ObjectList.Add(Instantiate(m_CustomObject, m_StartingPos, Quaternion.identity));
                        m_ObjectList[i].transform.SetParent(parent.transform); // This is to add in parent
                        if (m_UseLocalRotation)
                        {
                            m_ObjectList[i].transform.position = m_StartingPos + i * GetDirection() * m_Spacing;
                        }
                        else
                        {
                            m_ObjectList[i].transform.position = m_StartingPos + i * GetDirection() * m_Spacing;
                        }
                    }
                    else
                    {
                        m_ObjectList.Add(Instantiate(m_CustomObject, m_Transform.position, Quaternion.identity));
                        m_ObjectList[i].transform.SetParent(parent.transform);
                        if (m_UseLocalRotation)
                        {
                            m_ObjectList[i].transform.position = m_StartingPos + i * GetDirection() * m_Spacing;
                        }
                        else
                        {
                            m_ObjectList[i].transform.position = m_Transform.position + i * GetDirection() * m_Spacing;
                        }
                    }
                }
                m_ObjectList[i].name = m_Name; // To add the name of the gameobject
                m_ObjectList[i].AddComponent<Environnement>(); // To add the Environnement script

                Renderer rend = m_ObjectList[i].GetComponent<Renderer>();
                Material mat = new Material(rend.sharedMaterial);
                mat.color = Color.Lerp(m_StartingColor, m_EndColor, i / (float)m_NbToCreate);
                rend.sharedMaterial = mat;
            }
        }
        EditorGUILayout.EndVertical();
    }

    private Vector3 GetDirection()
    {
        switch (m_Direction)
        {
            case Direction.Up:
                return m_Transform == null ? Vector3.up : m_Transform.up;
            case Direction.Down:
                return m_Transform == null ? -Vector3.up : -m_Transform.up;
            case Direction.Right:
                return m_Transform == null ? Vector3.right : m_Transform.right;
            case Direction.Left:
                return m_Transform == null ? -Vector3.right : -m_Transform.right;
            case Direction.Forward:
                return m_Transform == null ? Vector3.forward : m_Transform.forward;
            case Direction.Back:
                return m_Transform == null ? -Vector3.forward : -m_Transform.forward;
            default:
                return Vector3.zero;
        }
    }
}
