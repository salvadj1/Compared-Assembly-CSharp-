using System;
using UnityEngine;

// Token: 0x020002F4 RID: 756
public interface IStateInterpolatorWithLinearVelocity
{
	// Token: 0x06001A74 RID: 6772
	bool SampleWorldVelocity(double timeStamp, out Vector3 linear);

	// Token: 0x06001A75 RID: 6773
	bool SampleWorldVelocity(out Vector3 linear);
}
