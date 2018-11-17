using System;

// Token: 0x02000321 RID: 801
public struct TimeStamped<T>
{
	// Token: 0x06001EBA RID: 7866 RVA: 0x00078BFC File Offset: 0x00076DFC
	public void Set(ref T value, ref double timeStamp)
	{
		this.timeStamp = timeStamp;
		this.value = value;
	}

	// Token: 0x04000ED5 RID: 3797
	public double timeStamp;

	// Token: 0x04000ED6 RID: 3798
	public int index;

	// Token: 0x04000ED7 RID: 3799
	public T value;
}
