using System;

// Token: 0x020002BB RID: 699
public interface IStateInterpolator<TSampleType> : IStateInterpolatorSampler<TSampleType>
{
	// Token: 0x060018EB RID: 6379
	void SetGoals(ref TSampleType sample, ref double timeStamp);

	// Token: 0x060018EC RID: 6380
	void SetGoals(ref TimeStamped<TSampleType> sample);
}
