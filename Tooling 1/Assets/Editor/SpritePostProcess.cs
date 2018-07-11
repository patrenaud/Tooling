using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpritePostProcess : AssetPostprocessor
{
	private void OnPostprocessSprites(Texture2D i_Texture, Sprite[] i_Sprites)
	{
		Debug.Log(i_Sprites.Length);		
	}

	private void OnPostprocessTexture(Texture2D i_Texture)
	{
		Debug.Log(i_Texture.width + " , " + i_Texture.height);
	}

}
