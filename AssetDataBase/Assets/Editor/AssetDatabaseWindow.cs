using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO; // Permet de gérer les paths pour les fichiers

public class AssetDatabaseWindow : EditorWindow
{
    private enum AssetAction
    {
        Copy,
        Move,
        Open,
        Rename
    }
    private const string TEXTURE_PATH = "Assets/Art/Pokemon.jpg";
    private const string SPRITE_SHEET_PATH = "Assets/Art/sonicsheet.png";

    private bool m_ShowTexture;
    private Texture m_Texture;

    private AssetAction m_Actions = AssetAction.Copy;
    private Object m_Asset;

    private Object m_Directory;
    private string m_Rename;

    private string m_Animal;
    private string[] m_PopupExample = new string[3] { "Dog", "Cat", "Hyppo" };

    [MenuItem("Tools/Asset Manipulator...")] // Création de l'onglet au menu
    private static void Init()
    {
        GetWindow<AssetDatabaseWindow>().Show(); // Pour afficher la window
    }

    private void OnEnable()
    {
        m_Texture = AssetDatabase.LoadAssetAtPath<Texture>(TEXTURE_PATH);
    }

    private void OnGUI()
    {
        ShowAsset();
        ShowActions();
        ShowMisc();
    }

    private void ShowAsset()
    {
        // Crée un slot pour insérer un object (Asset)
        m_Asset = EditorGUILayout.ObjectField("Asset", m_Asset, typeof(Object), false);

        // Si aucun asset, le bouton est disabled
        EditorGUI.BeginDisabledGroup(m_Asset == null);
        if (GUILayout.Button("Show Asset Path"))
        {
            string assetPath = AssetDatabase.GetAssetPath(m_Asset); // Sert donner le path de l'objet
            string extension = Path.GetExtension(assetPath); // *donne l'extension
            Debug.Log("Asset Path: " + assetPath + " | Extension: " + extension);
        }
        EditorGUI.EndDisabledGroup();
    }

    private void ShowActions()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box); // crée une box comme style
        EditorGUILayout.LabelField("Actions", EditorStyles.centeredGreyMiniLabel); // Regroupe en rectangle clean

        // Liste déroulante (EnumPopup). On doit le caster en type de notre Enum pour le créer.
        m_Actions = (AssetAction)EditorGUILayout.EnumPopup("Actions ", m_Actions);

        // Pour un array de string: On peut utiliser Popup à partir d'un string[]
        int index = ArrayUtility.IndexOf(m_PopupExample, m_Animal);
        index = EditorGUILayout.Popup("Animals", index, m_PopupExample);
        if (index != -1)
        {
            m_Animal = m_PopupExample[index];
        }

        ShowParameters();
        ShowActionButton();

        EditorGUILayout.EndVertical();
    }

    private void ShowParameters()
    {
        switch (m_Actions)
        {
            case AssetAction.Copy:
            case AssetAction.Move:
                DirectoryField();
                break;
            case AssetAction.Rename:
                m_Rename = EditorGUILayout.TextField("Rename", m_Rename);
                break;
        }
    }

    private void DirectoryField()
    {
        EditorGUI.BeginChangeCheck();
        m_Directory = EditorGUILayout.ObjectField("Directory", m_Directory, typeof(Object), false);
        if (EditorGUI.EndChangeCheck()) // Va donner le directory seulement quand le changement se fait
        {
            if (m_Directory != null)
            {
                string assetPath = AssetDatabase.GetAssetPath(m_Directory);
                bool isValid = AssetDatabase.IsValidFolder(assetPath);
                if (!isValid)
                {
                    Debug.LogWarning("This isn't a directory!");
                    m_Directory = null;
                }
            }
        }
    }

    private void ShowActionButton()
    {
        EditorGUI.BeginDisabledGroup(m_Asset == null);
        if (GUILayout.Button(m_Actions.ToString() + " asset"))
        {
            string assetPath = AssetDatabase.GetAssetPath(m_Asset);

            bool needSave = false; // Ceci est pour Save/Refresh après une opération

            switch (m_Actions)
            {
                case AssetAction.Copy:
                case AssetAction.Move:
                    if (m_Directory != null)
                    {
                        string directoryPath = AssetDatabase.GetAssetPath(m_Directory);
                        directoryPath += "/" + GetAssetFullName(assetPath);
                        if (assetPath.Equals(directoryPath))
                        {
                            Debug.LogError("Cannot " + m_Actions.ToString() + " since paths are the same!");
                            return;
                        }
                        if (m_Actions == AssetAction.Copy)
                        {
                            needSave = AssetDatabase.CopyAsset(assetPath, directoryPath);
                        }
                        else
                        {
                            needSave = string.IsNullOrEmpty(AssetDatabase.MoveAsset(assetPath, directoryPath));
                        }
                    }
                    break;
                case AssetAction.Open:
                    AssetDatabase.OpenAsset(m_Asset);
                    break;
                case AssetAction.Rename:
                    if (!string.IsNullOrEmpty(m_Rename))
                    {
                        needSave = string.IsNullOrEmpty(AssetDatabase.RenameAsset(assetPath, m_Rename));
                    }
                    break;
            }
            if (needSave)
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
        EditorGUI.EndDisabledGroup();
    }

    private void ShowMisc()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("Misc", EditorStyles.centeredGreyMiniLabel);

        m_ShowTexture = EditorGUILayout.Toggle("Show Texture?", m_ShowTexture);
        if (m_ShowTexture)
        {
            GUILayout.Label(m_Texture);
        }
        if (GUILayout.Button("Load Sprites"))
        {
            Object[] allObjects = AssetDatabase.LoadAllAssetsAtPath(SPRITE_SHEET_PATH);
            /*foreach (Object obj in allObjects)
            {
                Debug.Log("Name :" + obj.name + " | Type: " + obj.GetType().Name);
            }*/
        }

        EditorGUI.BeginDisabledGroup(m_Asset == null);
        if(GUILayout.Button("Get Dependencies"))
        {
            string assetPath = AssetDatabase.GetAssetPath(m_Asset);
            string[] dependencies = AssetDatabase.GetDependencies(assetPath, true);
            foreach (string s in dependencies)
            {
                Debug.Log(s);
            }
        }
         EditorGUI.EndDisabledGroup();

        EditorGUILayout.EndVertical();
    }

    private string GetAssetFullName(string i_AssetPath)
    {
        // Le split sert à chercher ce qui est après le dernier slash!
        string[] splits = i_AssetPath.Split('/');
        return splits[splits.Length - 1];
    }
}
