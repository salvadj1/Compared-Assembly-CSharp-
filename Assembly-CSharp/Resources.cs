using System;
using UnityEngine;

// Token: 0x020001A5 RID: 421
public static class Resources
{
	// Token: 0x06000C43 RID: 3139 RVA: 0x00030CE4 File Offset: 0x0002EEE4
	[Obsolete("Do not use Resources. Use Bundles.", false)]
	public static Object Load(string path)
	{
		return Resources.Load(path);
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x00030CEC File Offset: 0x0002EEEC
	[Obsolete("Do not use Resources. Use Bundles.", false)]
	public static Object Load(string path, Type type)
	{
		return Resources.Load(path, type);
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x00030CF8 File Offset: 0x0002EEF8
	[Obsolete("Do not use Resources. Use Bundles.", false)]
	public static Object[] LoadAll(string path)
	{
		return Resources.LoadAll(path);
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00030D00 File Offset: 0x0002EF00
	[Obsolete("Do not use Resources. Use Bundles.", false)]
	public static Object[] LoadAll(string path, Type type)
	{
		return Resources.LoadAll(path, type);
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x00030D0C File Offset: 0x0002EF0C
	public static void UnloadAsset(Object assetToUnload)
	{
		Resources.UnloadAsset(assetToUnload);
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00030D14 File Offset: 0x0002EF14
	public static AsyncOperation UnloadUnusedAssets()
	{
		return Resources.UnloadUnusedAssets();
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x00030D1C File Offset: 0x0002EF1C
	public static Object[] FindObjectsOfTypeAll(Type type)
	{
		return Resources.FindObjectsOfTypeAll(type);
	}

	// Token: 0x0400076E RID: 1902
	private const string kDontUse = "Do not use Resources. Use Bundles.";

	// Token: 0x0400076F RID: 1903
	private const bool kErrorNotWarning = false;
}
