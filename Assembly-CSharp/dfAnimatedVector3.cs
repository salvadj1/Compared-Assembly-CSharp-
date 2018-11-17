using System;
using UnityEngine;

// Token: 0x02000810 RID: 2064
public class dfAnimatedVector3 : global::dfAnimatedValue<Vector3>
{
	// Token: 0x060047A1 RID: 18337 RVA: 0x0010F1CC File Offset: 0x0010D3CC
	public dfAnimatedVector3(Vector3 StartValue, Vector3 EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x060047A2 RID: 18338 RVA: 0x0010F1D8 File Offset: 0x0010D3D8
	protected override Vector3 Lerp(Vector3 startValue, Vector3 endValue, float time)
	{
		return Vector3.Lerp(startValue, endValue, time);
	}

	// Token: 0x060047A3 RID: 18339 RVA: 0x0010F1E4 File Offset: 0x0010D3E4
	public static implicit operator global::dfAnimatedVector3(Vector3 value)
	{
		return new global::dfAnimatedVector3(value, value, 0f);
	}
}
