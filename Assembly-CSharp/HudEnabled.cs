using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005B6 RID: 1462
public class HudEnabled : MonoBehaviour
{
	// Token: 0x06002EED RID: 12013 RVA: 0x000B4E7C File Offset: 0x000B307C
	private void Awake()
	{
		GameObject gameObject = base.gameObject;
		global::HudEnabled.G.All.Add(gameObject);
		gameObject.SetActive(global::HudEnabled.On);
	}

	// Token: 0x06002EEE RID: 12014 RVA: 0x000B4EA8 File Offset: 0x000B30A8
	private void OnDestroy()
	{
		if (global::HudEnabled.GReady)
		{
			global::HudEnabled.G.All.Remove(base.gameObject);
		}
	}

	// Token: 0x06002EEF RID: 12015 RVA: 0x000B4EC8 File Offset: 0x000B30C8
	public static void Set(bool enable)
	{
		if (global::HudEnabled.On != enable)
		{
			global::HudEnabled.Toggle();
		}
	}

	// Token: 0x06002EF0 RID: 12016 RVA: 0x000B4EDC File Offset: 0x000B30DC
	public static void Toggle()
	{
		global::HudEnabled.On = !global::HudEnabled.On;
		if (global::HudEnabled.GReady)
		{
			foreach (GameObject gameObject in global::HudEnabled.G.All)
			{
				gameObject.SetActive(global::HudEnabled.On);
			}
		}
	}

	// Token: 0x06002EF1 RID: 12017 RVA: 0x000B4F5C File Offset: 0x000B315C
	public static void Enable()
	{
		global::HudEnabled.Set(true);
	}

	// Token: 0x06002EF2 RID: 12018 RVA: 0x000B4F64 File Offset: 0x000B3164
	public static void Disable()
	{
		global::HudEnabled.Set(false);
	}

	// Token: 0x04001978 RID: 6520
	private static bool On;

	// Token: 0x04001979 RID: 6521
	private static bool GReady;

	// Token: 0x020005B7 RID: 1463
	private static class G
	{
		// Token: 0x06002EF3 RID: 12019 RVA: 0x000B4F6C File Offset: 0x000B316C
		static G()
		{
			global::HudEnabled.GReady = true;
		}

		// Token: 0x0400197A RID: 6522
		public static readonly HashSet<GameObject> All = new HashSet<GameObject>();
	}
}
