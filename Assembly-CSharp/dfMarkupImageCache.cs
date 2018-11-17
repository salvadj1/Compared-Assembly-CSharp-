using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200071C RID: 1820
public class dfMarkupImageCache
{
	// Token: 0x060042AF RID: 17071 RVA: 0x00102AC8 File Offset: 0x00100CC8
	public static void Clear()
	{
		dfMarkupImageCache.cache.Clear();
	}

	// Token: 0x060042B0 RID: 17072 RVA: 0x00102AD4 File Offset: 0x00100CD4
	public static void Load(string name, Texture image)
	{
		dfMarkupImageCache.cache[name.ToLowerInvariant()] = image;
	}

	// Token: 0x060042B1 RID: 17073 RVA: 0x00102AE8 File Offset: 0x00100CE8
	public static void Unload(string name)
	{
		dfMarkupImageCache.cache.Remove(name.ToLowerInvariant());
	}

	// Token: 0x060042B2 RID: 17074 RVA: 0x00102AFC File Offset: 0x00100CFC
	public static Texture Load(string path)
	{
		path = path.ToLowerInvariant();
		if (dfMarkupImageCache.cache.ContainsKey(path))
		{
			return dfMarkupImageCache.cache[path];
		}
		Texture texture = Resources.Load(path) as Texture;
		if (texture != null)
		{
			dfMarkupImageCache.cache[path] = texture;
		}
		return texture;
	}

	// Token: 0x0400231F RID: 8991
	private static Dictionary<string, Texture> cache = new Dictionary<string, Texture>();
}
