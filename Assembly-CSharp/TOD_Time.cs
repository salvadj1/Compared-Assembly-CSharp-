using System;
using UnityEngine;

// Token: 0x02000930 RID: 2352
public class TOD_Time : MonoBehaviour
{
	// Token: 0x06004F71 RID: 20337 RVA: 0x00150024 File Offset: 0x0014E224
	protected void Start()
	{
		this.sky = base.GetComponent<global::TOD_Sky>();
	}

	// Token: 0x06004F72 RID: 20338 RVA: 0x00150034 File Offset: 0x0014E234
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

	// Token: 0x04002DFC RID: 11772
	public float DayLengthInMinutes = 30f;

	// Token: 0x04002DFD RID: 11773
	public bool ProgressDate = true;

	// Token: 0x04002DFE RID: 11774
	public bool ProgressMoonPhase = true;

	// Token: 0x04002DFF RID: 11775
	private global::TOD_Sky sky;
}
