using System;

// Token: 0x020003CE RID: 974
public struct TimeStamped<T>
{
	// Token: 0x0600221C RID: 8732 RVA: 0x0007DFF8 File Offset: 0x0007C1F8
	public void Set(ref T value, ref double timeStamp)
	{
		this.timeStamp = timeStamp;
		this.value = value;
	}

	// Token: 0x0400103B RID: 4155
	public double timeStamp;

	// Token: 0x0400103C RID: 4156
	public int index;

	// Token: 0x0400103D RID: 4157
	public T value;
}
