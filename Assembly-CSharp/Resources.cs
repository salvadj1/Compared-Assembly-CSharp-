using System;
using UnityEngine;

// Token: 0x020001D3 RID: 467
public static class Resources
{
	// Token: 0x06000D7B RID: 3451 RVA: 0x00034BD0 File Offset: 0x00032DD0
	[Obsolete("Do not use Resources. Use Bundles.", false)]
	public static Object Load(string path)
	{
		return UnityEngine.Resources.Load(path);
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x00034BD8 File Offset: 0x00032DD8
	[Obsolete("Do not use Resources. Use Bundles.", false)]
	public static Object Load(string path, Type type)
	{
		return UnityEngine.Resources.Load(path, type);
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x00034BE4 File Offset: 0x00032DE4
	[Obsolete("Do not use Resources. Use Bundles.", false)]
	public static Object[] LoadAll(string path)
	{
		return UnityEngine.Resources.LoadAll(path);
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x00034BEC File Offset: 0x00032DEC
	[Obsolete("Do not use Resources. Use Bundles.", false)]
	public static Object[] LoadAll(string path, Type type)
	{
		return UnityEngine.Resources.LoadAll(path, type);
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x00034BF8 File Offset: 0x00032DF8
	public static void UnloadAsset(Object assetToUnload)
	{
		UnityEngine.Resources.UnloadAsset(assetToUnload);
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x00034C00 File Offset: 0x00032E00
	public static AsyncOperation UnloadUnusedAssets()
	{
		return UnityEngine.Resources.UnloadUnusedAssets();
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x00034C08 File Offset: 0x00032E08
	public static Object[] FindObjectsOfTypeAll(Type type)
	{
		return UnityEngine.Resources.FindObjectsOfTypeAll(type);
	}

	// Token: 0x04000882 RID: 2178
	private const string kDontUse = "Do not use Resources. Use Bundles.";

	// Token: 0x04000883 RID: 2179
	private const bool kErrorNotWarning = false;
}
