using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cours2GUI : MonoBehaviour 
{
	public Texture m_MyTexture;
	public string[] m_MyStrings;
	public Rect m_WindowRect = new Rect(50f, 50f, 200f, 100f);
	public Texture2D m_BoxBackground;


	private bool m_ShowTexture;
	private int m_SelectionGridIndex;
	private Rect m_BoxRect;
	private float m_HorizontalSliderValue = 0.5f;
	private float m_VerticalSliderValue = 0.25f;
	private string m_Password = "";
	private Vector2 m_ScrollPosition;


	private void OnGUI()
	{
		m_BoxRect = new Rect(10f, 10f, Screen.width * m_HorizontalSliderValue, Screen.height * m_VerticalSliderValue);
		Rect scrollRect = m_BoxRect;
		scrollRect.height = 300f;
		Rect scrollViewRect = scrollRect;
		scrollViewRect.width += 20f;
		scrollViewRect.height += 20f;

		m_ScrollPosition = GUI.BeginScrollView(scrollRect, m_ScrollPosition, scrollViewRect);

		GUIStyle boxStyle = new  GUIStyle(GUI.skin.box);

		if(m_BoxBackground != null)
		{
			boxStyle.normal.background = m_BoxBackground;
		}
		GUI.Box(m_BoxRect, "My Box");
		m_BoxRect.height = 20f;

		SetNewLine();			

		GUIStyle labelStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
		GUIContent myContent = new GUIContent("My Label", "This is enabled");
		Vector2 labelSize = labelStyle.CalcSize(myContent);

		
		Rect labelRect = GetCenteredRect(m_BoxRect, labelSize.x);			
		GUI.Label(labelRect, myContent, labelStyle);	

		SetNewLine();

		Rect horizontalSliderRect = GetCenteredRect(m_BoxRect, m_BoxRect.width*0.5f);	
		m_HorizontalSliderValue = GUI.HorizontalSlider(horizontalSliderRect, m_HorizontalSliderValue, 0.1f, 1f);

		SetNewLine(100f);

		Rect verticalSliderrect = GetCenteredRect(m_BoxRect, 20f);	
		m_VerticalSliderValue = GUI.VerticalSlider(verticalSliderrect, m_VerticalSliderValue, 1f, 0.1f);

		SetNewLine();

		Rect passwordRect = GetCenteredRect(m_BoxRect);
		m_Password = GUI.PasswordField(passwordRect, m_Password, '*', 20);

		SetNewLine();

		Rect textRect = GetCenteredRect(m_BoxRect);
		m_Password = GUI.TextField(textRect, m_Password);

		SetNewLine(100f);

		Rect selectionRect = GetCenteredRect(m_BoxRect, m_BoxRect.width*0.5f);
        m_SelectionGridIndex = GUI.SelectionGrid(selectionRect, m_SelectionGridIndex, m_MyStrings, 2); 

		GUI.EndScrollView();

		m_WindowRect = GUI.Window(0, m_WindowRect, ShowWindow, "Window");

		ShowGroup();	
	}

	private void ShowWindow(int i_WindowID)
	{
		if(GUI.Button(new Rect(0f, 0f, 100f, 50f), "Uncle Ben"))
		{
			Debug.Log("Killed Uncle Ben");
		}

		Rect dragRect = m_WindowRect;
		dragRect.x = 0f;
		dragRect.y = 0f;
		GUI.DragWindow(dragRect); 
	}
	private Rect GetCenteredRect(Rect i_GroupRect, float i_Width = -1f)
	{
		Rect rect = i_GroupRect;

		//opérateur ternaire:
		//			   Condition ?   if true                         if false
		rect.width = i_Width < 0 ? rect.width = i_GroupRect.width : i_Width;
		
		rect.x += i_GroupRect.width*0.5f - rect.width*0.5f;

		return rect;
	}

	private void SetNewLine(float i_Height = 20f, float i_Spacing = 5f)
	{
		m_BoxRect.y += m_BoxRect.height + i_Spacing;
		m_BoxRect.height = i_Height;
		
	}

	private void ShowGroup()
	{
		Rect groupRect = new Rect(350f, 10f, m_MyTexture.width, m_MyTexture.height);

		GUI.BeginGroup(groupRect);
		{
			groupRect.x = 0f;
			groupRect.y = 0f;
			GUI.Box(groupRect, "");
			Rect buttonRect = new Rect(0f, 0f, 50f, 20f);

			// Un bouton appuyé renvoie un bool true:		
			if(GUI.Button(buttonRect, "Button"))
			{
				m_ShowTexture = !m_ShowTexture;				
			}

			if(m_ShowTexture)
			{
				Rect textureRect = new Rect(0f, 0f, m_MyTexture.width, m_MyTexture.height);
				GUI.DrawTexture(textureRect, m_MyTexture);
			}
			
		}
		
		GUI.EndGroup();
	}
	
}
