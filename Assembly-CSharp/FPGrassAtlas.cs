using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003E RID: 62
[ExecuteInEditMode]
public class FPGrassAtlas : ScriptableObject, IFPGrassAsset
{
	// Token: 0x06000235 RID: 565 RVA: 0x0000C758 File Offset: 0x0000A958
	private void OnEnable()
	{
		if (this.textures.Count == 0)
		{
			this.Initialize();
		}
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000C770 File Offset: 0x0000A970
	private void Initialize()
	{
		this.textures.Clear();
		this.properties.Clear();
		for (int i = 0; i < 16; i++)
		{
			this.textures.Add(null);
		}
		for (int j = 0; j < 16; j++)
		{
			this.properties.Add(ScriptableObject.CreateInstance<FPGrassProperty>());
		}
	}

	// Token: 0x04000167 RID: 359
	public const int max_textures = 16;

	// Token: 0x04000168 RID: 360
	public List<FPGrassProperty> properties = new List<FPGrassProperty>();

	// Token: 0x04000169 RID: 361
	public List<Texture2D> textures = new List<Texture2D>();

	// Token: 0x0400016A RID: 362
	public Texture2D textureAtlas;
}
