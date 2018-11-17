using System;

// Token: 0x020002B8 RID: 696
public interface IStateInterpolatorWithAngularVelocity
{
	// Token: 0x060018E6 RID: 6374
	bool SampleWorldVelocity(double timeStamp, out Angle2 angular);

	// Token: 0x060018E7 RID: 6375
	bool SampleWorldVelocity(out Angle2 angular);
}
