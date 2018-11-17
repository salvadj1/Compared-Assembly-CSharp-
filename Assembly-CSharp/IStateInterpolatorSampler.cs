using System;

// Token: 0x020002F7 RID: 759
public interface IStateInterpolatorSampler<TSampleType>
{
	// Token: 0x06001A7A RID: 6778
	bool Sample(ref double timeStamp, out TSampleType sample);
}
