using System;
using UnityEngine;

// Token: 0x020002B7 RID: 695
public interface IStateInterpolatorWithLinearVelocity
{
	// Token: 0x060018E4 RID: 6372
	bool SampleWorldVelocity(double timeStamp, out Vector3 linear);

	// Token: 0x060018E5 RID: 6373
	bool SampleWorldVelocity(out Vector3 linear);
}
