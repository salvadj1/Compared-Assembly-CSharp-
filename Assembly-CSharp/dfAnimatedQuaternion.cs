using System;
using UnityEngine;

// Token: 0x02000736 RID: 1846
public class dfAnimatedQuaternion : dfAnimatedValue<Quaternion>
{
	// Token: 0x06004362 RID: 17250 RVA: 0x00105F34 File Offset: 0x00104134
	public dfAnimatedQuaternion(Quaternion StartValue, Quaternion EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06004363 RID: 17251 RVA: 0x00105F40 File Offset: 0x00104140
	protected override Quaternion Lerp(Quaternion startValue, Quaternion endValue, float time)
	{
		return Quaternion.Lerp(startValue, endValue, time);
	}

	// Token: 0x06004364 RID: 17252 RVA: 0x00105F4C File Offset: 0x0010414C
	public static implicit operator dfAnimatedQuaternion(Quaternion value)
	{
		return new dfAnimatedQuaternion(value, value, 0f);
	}
}
