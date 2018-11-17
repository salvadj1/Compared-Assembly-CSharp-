using System;
using UnityEngine;

// Token: 0x02000842 RID: 2114
public class DeviceTime : MonoBehaviour
{
	// Token: 0x06004AC5 RID: 19141 RVA: 0x00146840 File Offset: 0x00144A40
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

	// Token: 0x04002BDA RID: 11226
	public TOD_Sky sky;
}
