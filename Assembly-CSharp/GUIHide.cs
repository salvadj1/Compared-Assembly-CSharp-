using System;
using UnityEngine;

// Token: 0x020003E6 RID: 998
public class GUIHide : MonoBehaviour
{
	// Token: 0x0600250B RID: 9483 RVA: 0x0008E498 File Offset: 0x0008C698
	public static void SetVisible(bool bShow)
	{
		Object[] array = Resources.FindObjectsOfTypeAll(typeof(GUIHide));
		foreach (GUIHide guihide in array)
		{
			if (guihide.gameObject == null)
			{
				return;
			}
			guihide.gameObject.SetActive(bShow);
		}
	}
}
