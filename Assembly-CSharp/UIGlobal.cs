using System;
using UnityEngine;

// Token: 0x0200079C RID: 1948
[AddComponentMenu("")]
public class UIGlobal : MonoBehaviour
{
	// Token: 0x06004671 RID: 18033 RVA: 0x00118480 File Offset: 0x00116680
	public static void EnsureGlobal()
	{
		if (Application.isPlaying && !UIGlobal.g)
		{
			GameObject gameObject = new GameObject("__UIGlobal", new Type[]
			{
				typeof(UIGlobal)
			});
			Object.DontDestroyOnLoad(gameObject);
			UIGlobal.g = gameObject.GetComponent<UIGlobal>();
		}
	}

	// Token: 0x06004672 RID: 18034 RVA: 0x001184D8 File Offset: 0x001166D8
	private void LateUpdate()
	{
		UIWidget.GlobalUpdate();
		UIPanel.GlobalUpdate();
	}

	// Token: 0x040026A4 RID: 9892
	private static UIGlobal g;
}
