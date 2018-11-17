using System;
using UnityEngine;

// Token: 0x0200083B RID: 2107
public class TOD_Time : MonoBehaviour
{
	// Token: 0x06004AB6 RID: 19126 RVA: 0x001460C0 File Offset: 0x001442C0
	protected void Start()
	{
		this.sky = base.GetComponent<TOD_Sky>();
	}

	// Token: 0x06004AB7 RID: 19127 RVA: 0x001460D0 File Offset: 0x001442D0
	protected void Update()
	{
		float num = this.DayLengthInMinutes * 60f;
		float num2 = num / 24f;
		float num3 = Time.deltaTime / num2;
		float num4 = Time.deltaTime / (30f * num) * 2f;
		this.sky.Cycle.Hour += num3;
		if (this.ProgressMoonPhase)
		{
			this.sky.Cycle.MoonPhase += num4;
			if (this.sky.Cycle.MoonPhase < -1f)
			{
				this.sky.Cycle.MoonPhase += 2f;
			}
			else if (this.sky.Cycle.MoonPhase > 1f)
			{
				this.sky.Cycle.MoonPhase -= 2f;
			}
		}
		if (this.sky.Cycle.Hour >= 24f)
		{
			this.sky.Cycle.Hour = 0f;
			if (this.ProgressDate)
			{
				int num5 = DateTime.DaysInMonth(this.sky.Cycle.Year, this.sky.Cycle.Month);
				if (++this.sky.Cycle.Day > num5)
				{
					this.sky.Cycle.Day = 1;
					if (++this.sky.Cycle.Month > 12)
					{
						this.sky.Cycle.Month = 1;
						this.sky.Cycle.Year++;
					}
				}
			}
		}
	}

	// Token: 0x04002BAE RID: 11182
	public float DayLengthInMinutes = 30f;

	// Token: 0x04002BAF RID: 11183
	public bool ProgressDate = true;

	// Token: 0x04002BB0 RID: 11184
	public bool ProgressMoonPhase = true;

	// Token: 0x04002BB1 RID: 11185
	private TOD_Sky sky;
}
