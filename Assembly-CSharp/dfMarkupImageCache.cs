using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007F8 RID: 2040
public class dfMarkupImageCache
{
	// Token: 0x060046F3 RID: 18163 RVA: 0x0010BDD8 File Offset: 0x00109FD8
	public static void Clear()
	{
		global::dfMarkupImageCache.cache.Clear();
	}

	// Token: 0x060046F4 RID: 18164 RVA: 0x0010BDE4 File Offset: 0x00109FE4
	public static void Load(string name, Texture image)
	{
		global::dfMarkupImageCache.cache[name.ToLowerInvariant()] = image;
	}

	// Token: 0x060046F5 RID: 18165 RVA: 0x0010BDF8 File Offset: 0x00109FF8
	public static void Unload(string name)
	{
		global::dfMarkupImageCache.cache.Remove(name.ToLowerInvariant());
	}

	// Token: 0x060046F6 RID: 18166 RVA: 0x0010BE0C File Offset: 0x0010A00C
	public static Texture Load(string path)
	{
		path = path.ToLowerInvariant();
		if (global::dfMarkupImageCache.cache.ContainsKey(path))
		{
			return global::dfMarkupImageCache.cache[path];
		}
		Texture texture = global::Resources.Load(path) as Texture;
		if (texture != null)
		{
			global::dfMarkupImageCache.cache[path] = texture;
		}
		return texture;
	}

	// Token: 0x04002542 RID: 9538
	private static Dictionary<string, Texture> cache = new Dictionary<string, Texture>();
}
