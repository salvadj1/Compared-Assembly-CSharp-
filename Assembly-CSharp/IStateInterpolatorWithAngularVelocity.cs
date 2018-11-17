using System;

// Token: 0x020002F5 RID: 757
public interface IStateInterpolatorWithAngularVelocity
{
	// Token: 0x06001A76 RID: 6774
	bool SampleWorldVelocity(double timeStamp, out global::Angle2 angular);

	// Token: 0x06001A77 RID: 6775
	bool SampleWorldVelocity(out global::Angle2 angular);
}
