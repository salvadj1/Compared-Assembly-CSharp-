using System;
using UnityEngine;

// Token: 0x0200080E RID: 2062
public class dfAnimatedFloat : global::dfAnimatedValue<float>
{
	// Token: 0x0600479B RID: 18331 RVA: 0x0010F174 File Offset: 0x0010D374
	public dfAnimatedFloat(float StartValue, float EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x0600479C RID: 18332 RVA: 0x0010F180 File Offset: 0x0010D380
	protected override float Lerp(float startValue, float endValue, float time)
	{
		return Mathf.Lerp(startValue, endValue, time);
	}

	// Token: 0x0600479D RID: 18333 RVA: 0x0010F18C File Offset: 0x0010D38C
	public static implicit operator global::dfAnimatedFloat(float value)
	{
		return new global::dfAnimatedFloat(value, value, 0f);
	}
}
