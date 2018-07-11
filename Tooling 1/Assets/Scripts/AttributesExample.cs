using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // Permet de voir le tout avant même de faire Play
[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))] // Maximum de 3 components
public class AttributesExample : MonoBehaviour
{
    #region Nested Classes
    [System.Serializable]
    public class Date
    {
        public int m_ID;
        public GameObject m_GO;
    }
    #endregion

    [Header("Group 1")] // Affiche un titre aux variables dans l'éditeur
    public float m_Myfloat;

    [SerializeField] // Reste private mais est visible dans l'éditeur	
    private float m_SerializedFloat;

    [HideInInspector]
    public float m_HideDloat;

    [Range(0, 1)]
    [Tooltip("This is used for nothing")] // Indique un message pour le paramètre
    public float m_Range;

    [Space(10f)] // crée un espace entre les headers
    [Header("Group 2")]
    [Multiline(5)] // 3 lignes par defauts
    public string m_MultiLine;

    [Clear]
    public string m_ClearString;

    public Date m_Data;

    private void OnGUI()
    {
        GUI.Label(new Rect(0f, 0f, 100, 20), "My Label");
    }
}
