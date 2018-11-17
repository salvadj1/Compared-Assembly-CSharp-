using System;

// Token: 0x020002BA RID: 698
public interface IStateInterpolatorSampler<TSampleType>
{
	// Token: 0x060018EA RID: 6378
	bool Sample(ref double timeStamp, out TSampleType sample);
}
