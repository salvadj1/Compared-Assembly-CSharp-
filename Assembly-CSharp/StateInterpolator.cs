using System;
using UnityEngine;

// Token: 0x020003D1 RID: 977
public abstract class StateInterpolator : global::BaseStateInterpolator
{
	// Token: 0x17000802 RID: 2050
	// (get) Token: 0x06002221 RID: 8737
	protected abstract double __storedDuration { get; }

	// Token: 0x17000803 RID: 2051
	// (get) Token: 0x06002222 RID: 8738
	protected abstract double __oldestTimeStamp { get; }

	// Token: 0x17000804 RID: 2052
	// (get) Token: 0x06002223 RID: 8739
	protected abstract double __newestTimeStamp { get; }

	// Token: 0x06002224 RID: 8740
	protected abstract void __Clear();

	// Token: 0x17000805 RID: 2053
	// (get) Token: 0x06002225 RID: 8741 RVA: 0x0007E074 File Offset: 0x0007C274
	public double storedDuration
	{
		get
		{
			return this.__storedDuration;
		}
	}

	// Token: 0x17000806 RID: 2054
	// (get) Token: 0x06002226 RID: 8742 RVA: 0x0007E07C File Offset: 0x0007C27C
	public double oldestTimeStamp
	{
		get
		{
			return this.__oldestTimeStamp;
		}
	}

	// Token: 0x17000807 RID: 2055
	// (get) Token: 0x06002227 RID: 8743 RVA: 0x0007E084 File Offset: 0x0007C284
	public double newestTimeStamp
	{
		get
		{
			return this.__newestTimeStamp;
		}
	}

	// Token: 0x06002228 RID: 8744 RVA: 0x0007E08C File Offset: 0x0007C28C
	public void Clear()
	{
		this.__Clear();
	}

	// Token: 0x04001040 RID: 4160
	[SerializeField]
	protected int _bufferCapacity = 32;

	// Token: 0x04001041 RID: 4161
	protected int len;
}
