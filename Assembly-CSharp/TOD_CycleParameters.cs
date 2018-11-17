using System;
using UnityEngine;

// Token: 0x0200091F RID: 2335
[Serializable]
public class TOD_CycleParameters
{
	// Token: 0x17000F26 RID: 3878
	// (get) Token: 0x06004F1C RID: 20252 RVA: 0x0014C950 File Offset: 0x0014AB50
	// (set) Token: 0x06004F1D RID: 20253 RVA: 0x0014C9A8 File Offset: 0x0014ABA8
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

	// Token: 0x17000F27 RID: 3879
	// (get) Token: 0x06004F1E RID: 20254 RVA: 0x0014CA08 File Offset: 0x0014AC08
	// (set) Token: 0x06004F1F RID: 20255 RVA: 0x0014CA24 File Offset: 0x0014AC24
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

	// Token: 0x06004F20 RID: 20256 RVA: 0x0014CA34 File Offset: 0x0014AC34
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

	// Token: 0x04002D69 RID: 11625
	public float Hour = 12f;

	// Token: 0x04002D6A RID: 11626
	public int Day = 1;

	// Token: 0x04002D6B RID: 11627
	public int Month = 3;

	// Token: 0x04002D6C RID: 11628
	public int Year = 2000;

	// Token: 0x04002D6D RID: 11629
	public float MoonPhase;

	// Token: 0x04002D6E RID: 11630
	public float Latitude;

	// Token: 0x04002D6F RID: 11631
	public float Longitude;

	// Token: 0x04002D70 RID: 11632
	public float UTC;
}
