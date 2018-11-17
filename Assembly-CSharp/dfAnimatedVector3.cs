using System;
using UnityEngine;

// Token: 0x02000733 RID: 1843
public class dfAnimatedVector3 : dfAnimatedValue<Vector3>
{
	// Token: 0x06004359 RID: 17241 RVA: 0x00105EBC File Offset: 0x001040BC
	public dfAnimatedVector3(Vector3 StartValue, Vector3 EndValue, float Time) : base(StartValue, EndValue, Time)
	{
	}

	// Token: 0x0600435A RID: 17242 RVA: 0x00105EC8 File Offset: 0x001040C8
	protected override Vector3 Lerp(Vector3 startValue, Vector3 endValue, float time)
	{
		return Vector3.Lerp(startValue, endValue, time);
	}

	// Token: 0x0600435B RID: 17243 RVA: 0x00105ED4 File Offset: 0x001040D4
	public static implicit operator dfAnimatedVector3(Vector3 value)
	{
		return new dfAnimatedVector3(value, value, 0f);
	}
}
