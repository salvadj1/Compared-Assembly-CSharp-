using System;
using UnityEngine;

// Token: 0x02000811 RID: 2065
public class dfAnimatedVector4 : global::dfAnimatedValue<Vector4>
{
	// Token: 0x060047A4 RID: 18340 RVA: 0x0010F1F4 File Offset: 0x0010D3F4
	public dfAnimatedVector4(Vector4 StartValue, Vector4 EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x060047A5 RID: 18341 RVA: 0x0010F200 File Offset: 0x0010D400
	protected override Vector4 Lerp(Vector4 startValue, Vector4 endValue, float time)
	{
		return Vector4.Lerp(startValue, endValue, time);
	}

	// Token: 0x060047A6 RID: 18342 RVA: 0x0010F20C File Offset: 0x0010D40C
	public static implicit operator global::dfAnimatedVector4(Vector4 value)
	{
		return new global::dfAnimatedVector4(value, value, 0f);
	}
}
