using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool1 : MonoBehaviour
{
    public Texture2D m_BoxBackground;
    public Color m_BoxStartColor = Color.white;
    public Color m_BoxEndColor = Color.white;
    public string[] m_ToolbarStrings;

    private Vector2 m_ScrollPosition;
    private int m_Toolbarint;
    private bool m_CheatToggle;
    private bool m_MyToggle;
    private float m_MyFloat;
    private string m_MyText;
    private Rect m_MyRect;
    private Color m_PreviousColor;
    private Texture2D m_TextureCopy;

    #region Getters
    private Color CurrentColor
    {
        get
        {
            return Color.Lerp(m_BoxStartColor, m_BoxEndColor, m_MyFloat);
        }
    }
    #endregion

    private void Start()
    {
        SetupTexture();
        m_PreviousColor.Equals(CurrentColor);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            m_CheatToggle = !m_CheatToggle;
        }

        if (!m_PreviousColor.Equals(CurrentColor))
        {
            SetColor();
        }
        m_PreviousColor = CurrentColor;
    }


    private void OnGUI()
    {


        // Assigne un style (texture) au box GUI       
        GUIStyle style = new GUIStyle(GUI.skin.box);
        if (m_TextureCopy != null)
        {
            style.normal.background = m_TextureCopy;
        }

        float relativewidth = Screen.width * 0.5f;

        // Ceci crée un scroll de l'entièreté du GUI
        m_ScrollPosition = GUI.BeginScrollView(new Rect(10f, 10f, relativewidth - 50f, 200f), m_ScrollPosition, new Rect(10f, 10f, relativewidth, 300f));

        // Création d'un Rectangle
        m_MyRect = new Rect(10f, 10f, 150f, 100f);


        // Box grise pour mieux lire le tout
        GUI.Box(new Rect(10f, 10f, relativewidth, 300f), " ", style);

        // Afficher un bouton en appuyant sur C (Voir Update)
        if (m_CheatToggle)
        {
            if (GUI.Button(m_MyRect, "Button"))
            {
                Debug.Log("ButtonClick");
            }
        }

        // Afficher un texte
        SetNewLine(20f);
        GUI.Label(m_MyRect, "MySlider");

        // Afficher un scroller horizontal
        SetNewLine(20f);
        m_MyFloat = GUI.HorizontalSlider(m_MyRect, m_MyFloat, 0f, 1f);

        // Afficher un box de text
        SetNewLine(50f);
        m_MyText = GUI.TextField(m_MyRect, m_MyText);

        // Afficher un Toggle
        SetNewLine(20f);
        m_MyToggle = GUI.Toggle(m_MyRect, m_MyToggle, "Toggle?");

        // Affiche des onglets
        SetNewLine(20f, 10f);
        int CurrentInt = m_Toolbarint;
        m_Toolbarint = GUI.Toolbar(m_MyRect, m_Toolbarint, m_ToolbarStrings);
        if (CurrentInt != m_Toolbarint)
        {
            OnToolbarChange();
        }

        // Ceci termine le scroll du GUI
        GUI.EndScrollView();
    }

    // Set le prochain Rect et s'assure de ne pas avoir d'overlapping
    private void SetNewLine(float i_Height = 100f, float i_Spacing = 15f)
    {
        m_MyRect.y += m_MyRect.height + i_Spacing;
        m_MyRect.height = i_Height;
    }


    // TEST
    private void SetColor()
    {
        if (m_TextureCopy == null)
        {
            return;
        }

        for (int y = 0; y < m_TextureCopy.height; y++)
        {
            for (int x = 0; x < m_TextureCopy.width; x++)
            {
                m_TextureCopy.SetPixel(x, y, CurrentColor);
            }
        }
        m_TextureCopy.Apply();
    }


    // Crée une copie de la texture pour ne pas planter les changements au runtime
    private void SetupTexture()
    {
        if (m_BoxBackground != null)
        {
            Color32[] pixels = m_BoxBackground.GetPixels32();
            System.Array.Reverse(pixels);
            m_TextureCopy = new Texture2D(m_BoxBackground.width, m_BoxBackground.height);
            m_TextureCopy.SetPixels32(pixels);
            m_TextureCopy.Apply();
        }
    }

    // Change le scale en appuyant sur les onglets
    private void OnToolbarChange()
    {
        Vector3 newScale = Vector3.zero;
        switch (m_Toolbarint)
        {
            case 0:
                newScale = Vector3.one * 0.1f;
                break;
            case 1:
                newScale = Vector3.one;
                break;
            case 2:
                newScale = Vector3.one * 2;
                break;
        }
        transform.localScale = newScale;
    }
}
