using System;
using UnityEngine;

// Token: 0x02000813 RID: 2067
public class dfAnimatedQuaternion : global::dfAnimatedValue<Quaternion>
{
	// Token: 0x060047AA RID: 18346 RVA: 0x0010F244 File Offset: 0x0010D444
	public dfAnimatedQuaternion(Quaternion StartValue, Quaternion EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x060047AB RID: 18347 RVA: 0x0010F250 File Offset: 0x0010D450
	protected override Quaternion Lerp(Quaternion startValue, Quaternion endValue, float time)
	{
		return Quaternion.Lerp(startValue, endValue, time);
	}

	// Token: 0x060047AC RID: 18348 RVA: 0x0010F25C File Offset: 0x0010D45C
	public static implicit operator global::dfAnimatedQuaternion(Quaternion value)
	{
		return new global::dfAnimatedQuaternion(value, value, 0f);
	}
}
