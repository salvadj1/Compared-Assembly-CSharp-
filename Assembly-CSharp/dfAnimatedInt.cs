using System;
using UnityEngine;

// Token: 0x02000732 RID: 1842
public class dfAnimatedInt : dfAnimatedValue<int>
{
	// Token: 0x06004356 RID: 17238 RVA: 0x00105E8C File Offset: 0x0010408C
	public dfAnimatedInt(int StartValue, int EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x06004357 RID: 17239 RVA: 0x00105E98 File Offset: 0x00104098
	protected override int Lerp(int startValue, int endValue, float time)
	{
		return Mathf.RoundToInt(Mathf.Lerp((float)startValue, (float)endValue, time));
	}

	// Token: 0x06004358 RID: 17240 RVA: 0x00105EAC File Offset: 0x001040AC
	public static implicit operator dfAnimatedInt(int value)
	{
		return new dfAnimatedInt(value, value, 0f);
	}
}
