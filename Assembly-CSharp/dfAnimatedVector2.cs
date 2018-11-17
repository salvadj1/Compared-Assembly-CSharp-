using System;
using UnityEngine;

// Token: 0x02000735 RID: 1845
public class dfAnimatedVector2 : dfAnimatedValue<Vector2>
{
	// Token: 0x0600435F RID: 17247 RVA: 0x00105F0C File Offset: 0x0010410C
	public dfAnimatedVector2(Vector2 StartValue, Vector2 EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06004360 RID: 17248 RVA: 0x00105F18 File Offset: 0x00104118
	protected override Vector2 Lerp(Vector2 startValue, Vector2 endValue, float time)
	{
		return Vector2.Lerp(startValue, endValue, time);
	}

	// Token: 0x06004361 RID: 17249 RVA: 0x00105F24 File Offset: 0x00104124
	public static implicit operator dfAnimatedVector2(Vector2 value)
	{
		return new dfAnimatedVector2(value, value, 0f);
	}
}
