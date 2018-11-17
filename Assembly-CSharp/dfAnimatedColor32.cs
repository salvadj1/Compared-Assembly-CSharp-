using System;
using UnityEngine;

// Token: 0x02000738 RID: 1848
public class dfAnimatedColor32 : dfAnimatedValue<Color32>
{
	// Token: 0x06004368 RID: 17256 RVA: 0x00105F84 File Offset: 0x00104184
	public dfAnimatedColor32(Color32 StartValue, Color32 EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06004369 RID: 17257 RVA: 0x00105F90 File Offset: 0x00104190
	protected override Color32 Lerp(Color32 startValue, Color32 endValue, float time)
	{
		return Color.Lerp(startValue, endValue, time);
	}

	// Token: 0x0600436A RID: 17258 RVA: 0x00105FAC File Offset: 0x001041AC
	public static implicit operator dfAnimatedColor32(Color32 value)
	{
		return new dfAnimatedColor32(value, value, 0f);
	}
}
