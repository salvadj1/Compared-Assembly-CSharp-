using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004F9 RID: 1273
public class HudEnabled : MonoBehaviour
{
	// Token: 0x06002B2D RID: 11053 RVA: 0x000ACDE0 File Offset: 0x000AAFE0
	private void Awake()
	{
		GameObject gameObject = base.gameObject;
		HudEnabled.G.All.Add(gameObject);
		gameObject.SetActive(HudEnabled.On);
	}

	// Token: 0x06002B2E RID: 11054 RVA: 0x000ACE0C File Offset: 0x000AB00C
	private void OnDestroy()
	{
		if (HudEnabled.GReady)
		{
			HudEnabled.G.All.Remove(base.gameObject);
		}
	}

	// Token: 0x06002B2F RID: 11055 RVA: 0x000ACE2C File Offset: 0x000AB02C
	public static void Set(bool enable)
	{
		if (HudEnabled.On != enable)
		{
			HudEnabled.Toggle();
		}
	}

	// Token: 0x06002B30 RID: 11056 RVA: 0x000ACE40 File Offset: 0x000AB040
	public static void Toggle()
	{
		HudEnabled.On = !HudEnabled.On;
		if (HudEnabled.GReady)
		{
			foreach (GameObject gameObject in HudEnabled.G.All)
			{
				gameObject.SetActive(HudEnabled.On);
			}
		}
	}

	// Token: 0x06002B31 RID: 11057 RVA: 0x000ACEC0 File Offset: 0x000AB0C0
	public static void Enable()
	{
		HudEnabled.Set(true);
	}

	// Token: 0x06002B32 RID: 11058 RVA: 0x000ACEC8 File Offset: 0x000AB0C8
	public static void Disable()
	{
		HudEnabled.Set(false);
	}

	// Token: 0x040017AC RID: 6060
	private static bool On;

	// Token: 0x040017AD RID: 6061
	private static bool GReady;

	// Token: 0x020004FA RID: 1274
	private static class G
	{
		// Token: 0x06002B33 RID: 11059 RVA: 0x000ACED0 File Offset: 0x000AB0D0
		static G()
		{
			HudEnabled.GReady = true;
		}

		// Token: 0x040017AE RID: 6062
		public static readonly HashSet<GameObject> All = new HashSet<GameObject>();
	}
}
