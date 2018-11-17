using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020001B5 RID: 437
public class Assert
{
	// Token: 0x06000D05 RID: 3333 RVA: 0x00032948 File Offset: 0x00030B48
	[Conditional("UNITY_EDITOR")]
	public static void Test(bool comparison, string message = "")
	{
		if (comparison)
		{
			return;
		}
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x00032954 File Offset: 0x00030B54
	[Conditional("UNITY_EDITOR")]
	public static void Throw(string message = "")
	{
		Debug.LogError(message);
		Debug.Break();
	}
}
