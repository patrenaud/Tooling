using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class OnGUITP1 : MonoBehaviour
{
    public Texture2D m_Blue;
    public Texture2D m_Green;
    public Texture2D m_Purple;
    public Texture2D m_White;
    public Texture2D m_Red;
    public Texture2D m_LeftButtonTexture;
    public Texture2D m_RightButtonTexture;
    public GameObject m_Player;
    [Space(10f)]
    
    public string[] m_ToolbarStrings;
    [Space(10f)]
    [Header("Color names")]
    public string[] m_TextureStrings;

    private Rect m_LeftButtonRect;
    private Rect m_RightButtonRect;
    private Rect m_BoxRect;
    private float m_HorizontalSliderValue = 0.5f;
    private bool m_MyToggle;
    private int m_Pages = 1;
    private int m_Toolbarint;
    private int m_SelectionGridIndex;
    private Vector3 m_CurrentSize;
    private Vector2 m_ScrollPosition;


    private void Start()
    {
        m_CurrentSize = m_Player.transform.localScale;
    }

    private void Update()
    {
        m_Player.transform.position = new Vector3(m_HorizontalSliderValue, 0f, 0) * 10;
        transform.localScale = m_CurrentSize;

        if (m_MyToggle)
        {
            m_Player.transform.localScale = Vector3.zero;
        }
        else
        {
            m_Player.transform.localScale = m_CurrentSize;
        }
    }

    private void OnGUI()
    {
        // This part is to get the main rect out
        m_BoxRect = new Rect(10f, 10f, Screen.width - 20f, Screen.height / 3f);
        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
        SetTexture(boxStyle);

        // This is the Title for the GUI depending on the button inputs
        if (m_Pages == 1)
        {
            GUI.Box(m_BoxRect, "Cheats", boxStyle);
        }
        else if (m_Pages == 2)
        {
            GUI.Box(m_BoxRect, "Stats", boxStyle);

        }
        else if (m_Pages == 3)
        {
            GUI.Box(m_BoxRect, "GUI", boxStyle);
        }



        if (m_Blue != null)
        {
            boxStyle.normal.background = m_Blue;
        }

        // These 2 if are for the 2 buttons and are changing the titles and the pages of the GUI
        m_LeftButtonRect = new Rect(m_BoxRect.x, m_BoxRect.y, Screen.width / 16, m_BoxRect.height);
        if (GUI.Button(m_LeftButtonRect, m_LeftButtonTexture))
        {
            if (m_Pages != 1)
            {
                m_Pages -= 1;
            }
            else if (m_Pages == 1)
            {
                m_Pages = 3;
            }
        }
        m_RightButtonRect = new Rect(Screen.width - (m_RightButtonRect.width + m_BoxRect.x), m_BoxRect.y, Screen.width / 16, m_BoxRect.height);
        if (GUI.Button(m_RightButtonRect, m_RightButtonTexture))
        {
            if (m_Pages != 3)
            {
                m_Pages += 1;
            }
            else if (m_Pages == 3)
            {
                m_Pages = 1;
            }
        }


        if (m_Pages == 1)
        {
            GUI.Box(m_BoxRect, "Cheats");

            if (m_Blue != null)
            {
                boxStyle.normal.background = m_Blue;
            }

            m_BoxRect.height = 20f;

            // This is for the position slider
            SetNewLine();
            Rect horizontalSliderRect = GetCenteredRect(m_BoxRect, m_BoxRect.width * 0.5f);
            m_HorizontalSliderValue = GUI.HorizontalSlider(horizontalSliderRect, m_HorizontalSliderValue, 0.2f, 1f);

            // This is for the Toggle
            SetNewLine();
            Rect togglerect = GetCenteredRect(m_BoxRect, 80f);
            m_MyToggle = GUI.Toggle(togglerect, m_MyToggle, "Disapear ?");

            // Toolbar
            SetNewLine(20f, 10f);
            int CurrentInt = m_Toolbarint;
            m_BoxRect.x += m_LeftButtonRect.width;
            m_BoxRect.width = m_BoxRect.width - 2 * m_LeftButtonRect.width;
            m_Toolbarint = GUI.Toolbar(m_BoxRect, m_Toolbarint, m_ToolbarStrings);
            if (CurrentInt != m_Toolbarint)
            {
                OnToolbarChange();
            }
        }
        else if (m_Pages == 2)
        {
            GUI.Box(m_BoxRect, "Stats");

            if (m_Blue != null)
            {
                boxStyle.normal.background = m_Blue;
            }

            m_BoxRect.height = 20f;
            m_BoxRect.width = Screen.width - m_LeftButtonRect.width * 2 - 20;
            m_BoxRect.x = m_LeftButtonRect.width + 10;
            SetNewLine();
            GUI.Box(m_BoxRect, "Local Position: " + m_Player.transform.position.ToString());

            SetNewLine();
            GUI.Box(m_BoxRect, "Local Rotation: " + m_Player.transform.rotation.ToString());

            SetNewLine();
            GUI.Box(m_BoxRect, "Local Scale: " + m_Player.transform.localScale.ToString());


        }
        else if (m_Pages == 3)
        {

            Rect scrollRect = m_BoxRect;
            Rect scrollViewRect = scrollRect;
            scrollViewRect.width = Screen.width - m_LeftButtonRect.width * 2 - 20;
            scrollViewRect.height += 80f;
            scrollRect.width = Screen.width - m_LeftButtonRect.width - 20;
            scrollRect.y += 20f;
            scrollRect.height = m_BoxRect.height - scrollRect.y;

            m_ScrollPosition = GUI.BeginScrollView(scrollRect, m_ScrollPosition, scrollViewRect);
            if (m_Blue != null)
            {
                boxStyle.normal.background = m_Blue;
            }

            Rect selectionRect = GetCenteredRect(m_BoxRect, m_BoxRect.width * 0.5f);
            m_SelectionGridIndex = GUI.SelectionGrid(selectionRect, m_SelectionGridIndex, m_TextureStrings, 1);



            GUI.EndScrollView();
        }
    }

    private void OnToolbarChange()
    {
        switch (m_Toolbarint)
        {
            case 0:
                m_CurrentSize = Vector3.one * 0.1f;
                break;
            case 1:
                m_CurrentSize = Vector3.one * 0.5f;
                break;
            case 2:
                m_CurrentSize = Vector3.one;
                break;
            case 3:
                m_CurrentSize = Vector3.one * 2f;
                break;
            case 4:
                m_CurrentSize = Vector3.one * 3f;
                break;
        }
    }


    private Rect GetCenteredRect(Rect i_GroupRect, float i_Width = -1f)
    {
        Rect rect = i_GroupRect;
        rect.width = i_Width < 0 ? rect.width = i_GroupRect.width : i_Width;
        rect.x += i_GroupRect.width * 0.5f - rect.width * 0.5f;
        return rect;
    }

    private void SetNewLine(float i_Height = 20f, float i_Spacing = 5f)
    {
        m_BoxRect.y += m_BoxRect.height + i_Spacing;
        m_BoxRect.height = i_Height;
    }

    public void SetTexture(GUIStyle boxstyle)
    {
        switch (m_SelectionGridIndex)
        {
            case 0:
                boxstyle.normal.background = m_Blue;
                break;
            case 1:
                boxstyle.normal.background = m_Green;
                break;
            case 2:
                boxstyle.normal.background = m_Purple;
                break;
            case 3:
                boxstyle.normal.background = m_White;
                break;
            case 4:
                boxstyle.normal.background = m_Red;
                break;

        }
    }
}


