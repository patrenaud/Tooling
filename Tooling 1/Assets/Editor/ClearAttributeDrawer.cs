using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(ClearAttribute))]
public class ClearAttributeDrawer : PropertyDrawer
{
    private const float BUTTON_SIZE = 20f;

    public override void OnGUI(Rect i_Position, SerializedProperty i_Property, GUIContent i_Label)
    {
        i_Position.width -= BUTTON_SIZE;

        i_Label = EditorGUI.BeginProperty(i_Position, i_Label, i_Property);
        {
            EditorGUI.PropertyField(i_Position, i_Property);
            Rect buttonrect = i_Position;
            buttonrect.width = BUTTON_SIZE;
            buttonrect.x += i_Position.width;

			GUI.color = Color.red;

            if (GUI.Button(buttonrect, "X"))
            {
                switch (i_Property.propertyType)
                {
                    case SerializedPropertyType.Color:
                        i_Property.colorValue = Color.white;
                        break;
                    case SerializedPropertyType.Integer:
                        i_Property.intValue = 0;
                        break;
                    case SerializedPropertyType.Float:
                        i_Property.floatValue = 0f;
                        break;
                    case SerializedPropertyType.ObjectReference:
                        i_Property.objectReferenceValue = null;
                        break;
                    case SerializedPropertyType.Quaternion:
                        i_Property.quaternionValue = Quaternion.identity;
                        break;
                    case SerializedPropertyType.String:
                        i_Property.stringValue = "";
                        break;
                    case SerializedPropertyType.Vector2:
                        i_Property.vector2Value = Vector2.zero;
                        break;
                    case SerializedPropertyType.Vector3:
                        i_Property.vector3Value = Vector3.zero;
                        break;					
                }
				GUI.FocusControl("");
            }
        }
		GUI.color = Color.white;
        EditorGUI.EndProperty();
    }
    //public virtual void OnGUI(Rect position, SerializedProperty property, GUIContent label);
}
