using System;
using UnityEngine;

// Token: 0x020002B9 RID: 697
public interface IStateInterpolatorWithVelocity : IStateInterpolatorWithLinearVelocity, IStateInterpolatorWithAngularVelocity
{
	// Token: 0x060018E8 RID: 6376
	bool SampleWorldVelocity(double timeStamp, out Vector3 linear, out Angle2 angular);

	// Token: 0x060018E9 RID: 6377
	bool SampleWorldVelocity(out Vector3 linear, out Angle2 angular);
}
