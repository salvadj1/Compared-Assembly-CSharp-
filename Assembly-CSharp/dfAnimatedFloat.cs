using System;
using UnityEngine;

// Token: 0x02000731 RID: 1841
public class dfAnimatedFloat : dfAnimatedValue<float>
{
	// Token: 0x06004353 RID: 17235 RVA: 0x00105E64 File Offset: 0x00104064
	public dfAnimatedFloat(float StartValue, float EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06004354 RID: 17236 RVA: 0x00105E70 File Offset: 0x00104070
	protected override float Lerp(float startValue, float endValue, float time)
	{
		return Mathf.Lerp(startValue, endValue, time);
	}

	// Token: 0x06004355 RID: 17237 RVA: 0x00105E7C File Offset: 0x0010407C
	public static implicit operator dfAnimatedFloat(float value)
	{
		return new dfAnimatedFloat(value, value, 0f);
	}
}
