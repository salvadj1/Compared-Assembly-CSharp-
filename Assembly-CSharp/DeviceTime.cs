using System;
using UnityEngine;

// Token: 0x02000937 RID: 2359
public class DeviceTime : MonoBehaviour
{
	// Token: 0x06004F80 RID: 20352 RVA: 0x001507A4 File Offset: 0x0014E9A4
	protected void OnEnable()
	{
		if (!this.sky)
		{
			Debug.LogError("Sky instance reference not set. Disabling script.");
			base.enabled = false;
		}
		else
		{
			DateTime now = DateTime.Now;
			this.sky.Cycle.Year = now.Year;
			this.sky.Cycle.Month = now.Month;
			this.sky.Cycle.Day = now.Day;
			this.sky.Cycle.Hour = (float)now.Hour + (float)now.Minute / 60f;
		}
	}

	// Token: 0x04002E28 RID: 11816
	public global::TOD_Sky sky;
}
