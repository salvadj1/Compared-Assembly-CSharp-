using System;
using UnityEngine;

// Token: 0x02000737 RID: 1847
public class dfAnimatedColor : dfAnimatedValue<Color>
{
	// Token: 0x06004365 RID: 17253 RVA: 0x00105F5C File Offset: 0x0010415C
	public dfAnimatedColor(Color StartValue, Color EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06004366 RID: 17254 RVA: 0x00105F68 File Offset: 0x00104168
	protected override Color Lerp(Color startValue, Color endValue, float time)
	{
		return Color.Lerp(startValue, endValue, time);
	}

	// Token: 0x06004367 RID: 17255 RVA: 0x00105F74 File Offset: 0x00104174
	public static implicit operator dfAnimatedColor(Color value)
	{
		return new dfAnimatedColor(value, value, 0f);
	}
}
