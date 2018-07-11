using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class AllAssetsPostProcess : AssetPostprocessor
{
    private static void OnPostprocessAllAssets(string[] i_ImportedAssets, string[] i_DeletedAssets, string[] i_MovedAssets, string[] i_MovedFromAssets)
    {
        foreach (string str in i_ImportedAssets)
        {
            Debug.Log("Reimported Assets: " + str);
        }

        foreach (string str in i_DeletedAssets)
        {
            Debug.Log("Deleted Assets: " + str);
        }

        for(int i = 0; i < i_MovedAssets.Length; i++)
        {
            Debug.Log("Moved Assets: " + i_MovedAssets[i] + " from: " + i_MovedFromAssets[i]);
        }
    }

}
