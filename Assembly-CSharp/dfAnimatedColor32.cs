using System;
using UnityEngine;

// Token: 0x02000815 RID: 2069
public class dfAnimatedColor32 : global::dfAnimatedValue<Color32>
{
	// Token: 0x060047B0 RID: 18352 RVA: 0x0010F294 File Offset: 0x0010D494
	public dfAnimatedColor32(Color32 StartValue, Color32 EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x060047B1 RID: 18353 RVA: 0x0010F2A0 File Offset: 0x0010D4A0
	protected override Color32 Lerp(Color32 startValue, Color32 endValue, float time)
	{
		return Color.Lerp(startValue, endValue, time);
	}

	// Token: 0x060047B2 RID: 18354 RVA: 0x0010F2BC File Offset: 0x0010D4BC
	public static implicit operator global::dfAnimatedColor32(Color32 value)
	{
		return new global::dfAnimatedColor32(value, value, 0f);
	}
}
