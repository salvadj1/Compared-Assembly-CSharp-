using System;
using UnityEngine;

// Token: 0x02000734 RID: 1844
public class dfAnimatedVector4 : dfAnimatedValue<Vector4>
{
	// Token: 0x0600435C RID: 17244 RVA: 0x00105EE4 File Offset: 0x001040E4
	public dfAnimatedVector4(Vector4 StartValue, Vector4 EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x0600435D RID: 17245 RVA: 0x00105EF0 File Offset: 0x001040F0
	protected override Vector4 Lerp(Vector4 startValue, Vector4 endValue, float time)
	{
		return Vector4.Lerp(startValue, endValue, time);
	}

	// Token: 0x0600435E RID: 17246 RVA: 0x00105EFC File Offset: 0x001040FC
	public static implicit operator dfAnimatedVector4(Vector4 value)
	{
		return new dfAnimatedVector4(value, value, 0f);
	}
}
