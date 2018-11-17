using System;
using UnityEngine;

// Token: 0x02000887 RID: 2183
[AddComponentMenu("")]
public class UIGlobal : MonoBehaviour
{
	// Token: 0x06004AF6 RID: 19190 RVA: 0x00121E00 File Offset: 0x00120000
	public static void EnsureGlobal()
	{
		if (Application.isPlaying && !global::UIGlobal.g)
		{
			GameObject gameObject = new GameObject("__UIGlobal", new Type[]
			{
				typeof(global::UIGlobal)
			});
			Object.DontDestroyOnLoad(gameObject);
			global::UIGlobal.g = gameObject.GetComponent<global::UIGlobal>();
		}
	}

	// Token: 0x06004AF7 RID: 19191 RVA: 0x00121E58 File Offset: 0x00120058
	private void LateUpdate()
	{
		global::UIWidget.GlobalUpdate();
		global::UIPanel.GlobalUpdate();
	}

	// Token: 0x040028DB RID: 10459
	private static global::UIGlobal g;
}
