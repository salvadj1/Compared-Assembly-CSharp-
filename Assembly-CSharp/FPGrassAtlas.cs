using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000050 RID: 80
[ExecuteInEditMode]
public class FPGrassAtlas : ScriptableObject, global::IFPGrassAsset
{
	// Token: 0x060002A7 RID: 679 RVA: 0x0000DD00 File Offset: 0x0000BF00
	private void OnEnable()
	{
		if (this.textures.Count == 0)
		{
			this.Initialize();
		}
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0000DD18 File Offset: 0x0000BF18
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
			this.properties.Add(ScriptableObject.CreateInstance<global::FPGrassProperty>());
		}
	}

	// Token: 0x040001C9 RID: 457
	public const int max_textures = 16;

	// Token: 0x040001CA RID: 458
	public List<global::FPGrassProperty> properties = new List<global::FPGrassProperty>();

	// Token: 0x040001CB RID: 459
	public List<Texture2D> textures = new List<Texture2D>();

	// Token: 0x040001CC RID: 460
	public Texture2D textureAtlas;
}
