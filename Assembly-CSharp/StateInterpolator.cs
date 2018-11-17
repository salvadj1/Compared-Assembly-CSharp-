using System;
using UnityEngine;

// Token: 0x02000324 RID: 804
public abstract class StateInterpolator : BaseStateInterpolator
{
	// Token: 0x170007A4 RID: 1956
	// (get) Token: 0x06001EBF RID: 7871
	protected abstract double __storedDuration { get; }

	// Token: 0x170007A5 RID: 1957
	// (get) Token: 0x06001EC0 RID: 7872
	protected abstract double __oldestTimeStamp { get; }

	// Token: 0x170007A6 RID: 1958
	// (get) Token: 0x06001EC1 RID: 7873
	protected abstract double __newestTimeStamp { get; }

	// Token: 0x06001EC2 RID: 7874
	protected abstract void __Clear();

	// Token: 0x170007A7 RID: 1959
	// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x00078C78 File Offset: 0x00076E78
	public double storedDuration
	{
		get
		{
			return this.__storedDuration;
		}
	}

	// Token: 0x170007A8 RID: 1960
	// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x00078C80 File Offset: 0x00076E80
	public double oldestTimeStamp
	{
		get
		{
			return this.__oldestTimeStamp;
		}
	}

	// Token: 0x170007A9 RID: 1961
	// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x00078C88 File Offset: 0x00076E88
	public double newestTimeStamp
	{
		get
		{
			return this.__newestTimeStamp;
		}
	}

	// Token: 0x06001EC6 RID: 7878 RVA: 0x00078C90 File Offset: 0x00076E90
	public void Clear()
	{
		this.__Clear();
	}

	// Token: 0x04000EDA RID: 3802
	[SerializeField]
	protected int _bufferCapacity = 32;

	// Token: 0x04000EDB RID: 3803
	protected int len;
}
