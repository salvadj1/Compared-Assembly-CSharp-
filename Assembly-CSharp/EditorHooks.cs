using System;
using System.Diagnostics;
using EditorHooksPrivate;
using UnityEngine;

// Token: 0x02000406 RID: 1030
public static class EditorHooks
{
	// Token: 0x06002470 RID: 9328 RVA: 0x00086B68 File Offset: 0x00084D68
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

	// Token: 0x06002471 RID: 9329 RVA: 0x00086BC4 File Offset: 0x00084DC4
	[Conditional("UNITY_EDITOR")]
	public static void SetDirty(this Object obj)
	{
		if (EditorHooksPrivate.Hooks._SetDirty != null)
		{
			EditorHooksPrivate.Hooks._SetDirty(obj);
		}
	}
}
