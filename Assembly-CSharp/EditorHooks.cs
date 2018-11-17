using System;
using System.Diagnostics;
using EditorHooksPrivate;
using UnityEngine;

// Token: 0x02000359 RID: 857
public static class EditorHooks
{
	// Token: 0x0600210E RID: 8462 RVA: 0x0008176C File Offset: 0x0007F96C
	static EditorHooks()
	{
		Type type = Type.GetType("EditorHooksEditor, Assembly-CSharp-Editor");
		try
		{
			if (type != null)
			{
				type.TypeInitializer.Invoke(null, null);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x0600210F RID: 8463 RVA: 0x000817C8 File Offset: 0x0007F9C8
	[Conditional("UNITY_EDITOR")]
	public static void SetDirty(this Object obj)
	{
		if (Hooks._SetDirty != null)
		{
			Hooks._SetDirty(obj);
		}
	}
}
