using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200080A RID: 2058
public static class TextureMaterial
{
	// Token: 0x060049CD RID: 18893 RVA: 0x0013A72C File Offset: 0x0013892C
	public static Material GetMaterial(Material skeleton, Texture mainTex)
	{
		if (!skeleton)
		{
			return null;
		}
		Dictionary<Texture, Material> dictionary;
		if (!TextureMaterial.dict.TryGetValue(skeleton, out dictionary))
		{
			Material material = new Material(skeleton);
			material.mainTexture = mainTex;
			dictionary = new Dictionary<Texture, Material>();
			dictionary.Add(mainTex, material);
			TextureMaterial.dict.Add(skeleton, dictionary);
			return material;
		}
		Material result;
		if (!dictionary.TryGetValue(mainTex, out result))
		{
			Material material2 = new Material(skeleton);
			material2.mainTexture = mainTex;
			dictionary.Add(mainTex, material2);
			return material2;
		}
		return result;
	}

	// Token: 0x040029C3 RID: 10691
	private static Dictionary<Material, Dictionary<Texture, Material>> dict = new Dictionary<Material, Dictionary<Texture, Material>>();
}
