using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000189 RID: 393
public class Assert
{
	// Token: 0x06000BD5 RID: 3029 RVA: 0x0002EA5C File Offset: 0x0002CC5C
	[Conditional("UNITY_EDITOR")]
	public static void Test(bool comparison, string message = "")
	{
		if (comparison)
		{
			return;
		}
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x0002EA68 File Offset: 0x0002CC68
	[Conditional("UNITY_EDITOR")]
	public static void Throw(string message = "")
	{
		Debug.LogError(message);
		Debug.Break();
	}
}
