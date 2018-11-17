using System;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public interface IStateInterpolatorWithVelocity : global::IStateInterpolatorWithLinearVelocity, global::IStateInterpolatorWithAngularVelocity
{
	// Token: 0x06001A78 RID: 6776
	bool SampleWorldVelocity(double timeStamp, out Vector3 linear, out global::Angle2 angular);

	// Token: 0x06001A79 RID: 6777
	bool SampleWorldVelocity(out Vector3 linear, out global::Angle2 angular);
}
