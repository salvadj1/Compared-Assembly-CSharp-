using System;
using UnityEngine;

// Token: 0x02000812 RID: 2066
public class dfAnimatedVector2 : global::dfAnimatedValue<Vector2>
{
	// Token: 0x060047A7 RID: 18343 RVA: 0x0010F21C File Offset: 0x0010D41C
	public dfAnimatedVector2(Vector2 StartValue, Vector2 EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x060047A8 RID: 18344 RVA: 0x0010F228 File Offset: 0x0010D428
	protected override Vector2 Lerp(Vector2 startValue, Vector2 endValue, float time)
	{
		return Vector2.Lerp(startValue, endValue, time);
	}

	// Token: 0x060047A9 RID: 18345 RVA: 0x0010F234 File Offset: 0x0010D434
	public static implicit operator global::dfAnimatedVector2(Vector2 value)
	{
		return new global::dfAnimatedVector2(value, value, 0f);
	}
}
