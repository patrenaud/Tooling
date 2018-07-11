using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class EditorWindowExample : EditorWindow
{
    private GameObject m_Target;
    private Dictionary<Transform, Component[]> m_Components;
    private bool[] m_Foldouts;
    private string m_Filter;

    // Cet attribut crée un menu tools dans l'editeur de Unity
    [MenuItem("Tools/EditorExample")]
    private static void Init()
    {
        EditorWindowExample example = GetWindow<EditorWindowExample>();
        example.Show();
    }

    private void OnGUI()
    {
        // Ceci demande à Unity de détecter les changements
        EditorGUI.BeginChangeCheck();

        // On cast l'objectfield en GameOnbect. le true signifie qu'on peut utiliser les objets de la scene
        m_Target = (GameObject)EditorGUILayout.ObjectField("Target", m_Target, typeof(GameObject), true);





        // Cette condition permet de rester hook sans avoir à redragger l'objet
        if (EditorGUI.EndChangeCheck() || (m_Target != null && m_Components == null))
        {
            if (m_Target != null)
            {
                // Le true va afficher les scripts inactifs
                Transform[] allTransforms = m_Target.GetComponentsInChildren<Transform>(true);

                m_Components = new Dictionary<Transform, Component[]>();
                for (int i = 0; i < allTransforms.Length; i++)
                {
                    Component[] components = allTransforms[i].GetComponents<Component>();
                    m_Components.Add(allTransforms[i], components);
                }
                m_Foldouts = new bool[allTransforms.Length];
            }
        }

        EditorGUI.BeginDisabledGroup(m_Target == null);


        // Pour les boutons de collapse
        EditorGUILayout.BeginHorizontal();
        GUI.color = Color.cyan;
        if (GUILayout.Button("Expand All"))
        {
            for (int i = 0; i < m_Foldouts.Length; i++)
            {
                m_Foldouts[i] = true;
            }
        }
        GUI.color = Color.magenta;
        if (GUILayout.Button("Collapse All"))
        {
            for (int i = 0; i < m_Foldouts.Length; i++)
            {
                m_Foldouts[i] = false;
            }
        }
        GUI.color = Color.white;
        EditorGUILayout.EndHorizontal();

        // Ceci est la searchzone pour trouger un component en particulier
        m_Filter = EditorGUILayout.TextField("Filter", m_Filter);

        EditorGUI.EndDisabledGroup();

        ShowComponents();
    }

    private void ShowComponents()
    {
        if (m_Target == null || m_Components == null)
        {
            return;
        }

        int index = 0;

        // if (m_Components.ContainKey()) peut servir à trouver une certaine Key dans l'array
        foreach (KeyValuePair<Transform, Component[]> kvp in m_Components)
        {
            bool isMissing = IsComponentMissing(kvp.Value);
            GUI.color = isMissing ? Color.red : Color.white;

            // Fait apparaître une boite pour rendre le tout plus beau
            EditorGUILayout.BeginHorizontal(GUI.skin.box);

            // Crée la petite flèche pour un menu déroulant
            m_Foldouts[index] = EditorGUILayout.Foldout(m_Foldouts[index], kvp.Key.name, true);

            EditorGUILayout.EndHorizontal();

            // // Crée un Identation pour chaque index
            // EditorGUI.indentLevel++;
            GUI.color = Color.white;

            if (m_Foldouts[index] || !string.IsNullOrEmpty(m_Filter))
            {
                for (int i = 0; i < kvp.Value.Length; i++)
                {
                    Component comp = kvp.Value[i];

                    if (!IsComponentContained(comp))
                    {
                        continue;
                    }

                    // Ceci est pour l'indentation de boutons
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(20f * (EditorGUI.indentLevel + 1));

                    string compName = GetComponentName(comp);

                    // EditorGUILayout.LabelField(compName); Ceci affiche le nom

                    // Ceci est pour le style du bouton
                    GUIStyle btnStyle = new GUIStyle(EditorStyles.toolbarButton);
                    btnStyle.alignment = TextAnchor.MiddleLeft;

                    GUI.color = comp == null ? Color.red : Color.white;
                    // Ceci focus l'objet
                    if (GUILayout.Button(compName, btnStyle))
                    {
                        Selection.activeGameObject = kvp.Key.gameObject;
                        EditorGUIUtility.PingObject(kvp.Key.gameObject);
                    }
                    GUI.color = Color.white;

                    EditorGUILayout.EndHorizontal();
                }
            }
            // // Ferme l'identation pour chaque index
            // EditorGUI.indentLevel--;
            index++;
        }
    }

    private bool IsComponentContained(Component i_Component)
    {
        if (string.IsNullOrEmpty(m_Filter))
        {
            return true;
        }

        string compName = GetComponentName(i_Component);
        return compName.ToLower().Contains(m_Filter.ToLower());
    }

    private string GetComponentName(Component i_Component)
    {
        return i_Component == null ? "Missing Script" : i_Component.GetType().Name;
    }

    private bool IsComponentMissing(Component[] i_Components)
    {
        return i_Components
        .Any(comp => comp == null);
    }
}
