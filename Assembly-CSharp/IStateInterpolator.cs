using System;

// Token: 0x020002F8 RID: 760
public interface IStateInterpolator<TSampleType> : global::IStateInterpolatorSampler<TSampleType>
{
	// Token: 0x06001A7B RID: 6779
	void SetGoals(ref TSampleType sample, ref double timeStamp);

	// Token: 0x06001A7C RID: 6780
	void SetGoals(ref global::TimeStamped<TSampleType> sample);
}
