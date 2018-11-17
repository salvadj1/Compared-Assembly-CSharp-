using System;
using UnityEngine;

// Token: 0x0200082A RID: 2090
[Serializable]
public class TOD_CycleParameters
{
	// Token: 0x17000E8C RID: 3724
	// (get) Token: 0x06004A61 RID: 19041 RVA: 0x001429EC File Offset: 0x00140BEC
	// (set) Token: 0x06004A62 RID: 19042 RVA: 0x00142A44 File Offset: 0x00140C44
	public DateTime DateTime
	{
		get
		{
			this.CheckRange();
			int num = (int)this.Hour;
			float num2 = (this.Hour - (float)num) * 60f;
			int num3 = (int)num2;
			float num4 = (num2 - (float)num3) * 60f;
			int second = (int)num4;
			return new DateTime(this.Year, this.Month, this.Day, num, num3, second);
		}
		set
		{
			this.Year = value.Year;
			this.Month = value.Month;
			this.Day = value.Day;
			this.Hour = (float)value.Hour + (float)value.Minute / 60f + (float)value.Second / 3600f;
		}
	}

	// Token: 0x17000E8D RID: 3725
	// (get) Token: 0x06004A63 RID: 19043 RVA: 0x00142AA4 File Offset: 0x00140CA4
	// (set) Token: 0x06004A64 RID: 19044 RVA: 0x00142AC0 File Offset: 0x00140CC0
	public long Ticks
	{
		get
		{
			return this.DateTime.Ticks;
		}
		set
		{
			this.DateTime = new DateTime(value);
		}
	}

	// Token: 0x06004A65 RID: 19045 RVA: 0x00142AD0 File Offset: 0x00140CD0
	public void CheckRange()
	{
		this.Year = Mathf.Clamp(this.Year, 1, 9999);
		this.Month = Mathf.Clamp(this.Month, 1, 12);
		this.Day = Mathf.Clamp(this.Day, 1, DateTime.DaysInMonth(this.Year, this.Month));
		this.Hour = Mathf.Repeat(this.Hour, 24f);
		this.Longitude = Mathf.Clamp(this.Longitude, -180f, 180f);
		this.Latitude = Mathf.Clamp(this.Latitude, -90f, 90f);
		this.MoonPhase = Mathf.Clamp(this.MoonPhase, -1f, 1f);
	}

	// Token: 0x04002B1B RID: 11035
	public float Hour = 12f;

	// Token: 0x04002B1C RID: 11036
	public int Day = 1;

	// Token: 0x04002B1D RID: 11037
	public int Month = 3;

	// Token: 0x04002B1E RID: 11038
	public int Year = 2000;

	// Token: 0x04002B1F RID: 11039
	public float MoonPhase;

	// Token: 0x04002B20 RID: 11040
	public float Latitude;

	// Token: 0x04002B21 RID: 11041
	public float Longitude;

	// Token: 0x04002B22 RID: 11042
	public float UTC;
}
