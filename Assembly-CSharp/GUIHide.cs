using System;
using UnityEngine;

// Token: 0x02000496 RID: 1174
public class GUIHide : MonoBehaviour
{
	// Token: 0x0600287D RID: 10365 RVA: 0x00093E84 File Offset: 0x00092084
	public static void SetVisible(bool bShow)
	{
		Object[] array = global::Resources.FindObjectsOfTypeAll(typeof(global::GUIHide));
		foreach (global::GUIHide guihide in array)
		{
			if (guihide.gameObject == null)
			{
				return;
			}
			guihide.gameObject.SetActive(bShow);
		}
	}
}
