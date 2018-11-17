using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008FC RID: 2300
public static class TextureMaterial
{
	// Token: 0x06004E7C RID: 20092 RVA: 0x00144690 File Offset: 0x00142890
	public static Material GetMaterial(Material skeleton, Texture mainTex)
	{
		if (!skeleton)
		{
			return null;
		}
		Dictionary<Texture, Material> dictionary;
		if (!global::TextureMaterial.dict.TryGetValue(skeleton, out dictionary))
		{
			Material material = new Material(skeleton);
			material.mainTexture = mainTex;
			dictionary = new Dictionary<Texture, Material>();
			dictionary.Add(mainTex, material);
			global::TextureMaterial.dict.Add(skeleton, dictionary);
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

	// Token: 0x04002C11 RID: 11281
	private static Dictionary<Material, Dictionary<Texture, Material>> dict = new Dictionary<Material, Dictionary<Texture, Material>>();
}
