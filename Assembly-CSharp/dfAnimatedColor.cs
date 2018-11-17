using System;
using UnityEngine;

// Token: 0x02000814 RID: 2068
public class dfAnimatedColor : global::dfAnimatedValue<Color>
{
	// Token: 0x060047AD RID: 18349 RVA: 0x0010F26C File Offset: 0x0010D46C
	public dfAnimatedColor(Color StartValue, Color EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x060047AE RID: 18350 RVA: 0x0010F278 File Offset: 0x0010D478
	protected override Color Lerp(Color startValue, Color endValue, float time)
	{
		return Color.Lerp(startValue, endValue, time);
	}

	// Token: 0x060047AF RID: 18351 RVA: 0x0010F284 File Offset: 0x0010D484
	public static implicit operator global::dfAnimatedColor(Color value)
	{
		return new global::dfAnimatedColor(value, value, 0f);
	}
}
