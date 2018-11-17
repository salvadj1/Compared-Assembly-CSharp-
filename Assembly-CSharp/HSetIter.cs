using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000363 RID: 867
public struct HSetIter<T> : IDisposable, IEnumerator, IEnumerator<T>
{
	// Token: 0x0600212E RID: 8494 RVA: 0x00081E50 File Offset: 0x00080050
	public HSetIter(HashSet<T>.Enumerator enumerator)
	{
		this.enumerator = enumerator;
	}

	// Token: 0x17000825 RID: 2085
	// (get) Token: 0x0600212F RID: 8495 RVA: 0x00081E5C File Offset: 0x0008005C
	object IEnumerator.Current
	{
		get
		{
			return this.enumerator.Current;
		}
	}

	// Token: 0x06002130 RID: 8496 RVA: 0x00081E70 File Offset: 0x00080070
	public bool MoveNext()
	{
		return this.enumerator.MoveNext();
	}

	// Token: 0x17000826 RID: 2086
	// (get) Token: 0x06002131 RID: 8497 RVA: 0x00081E80 File Offset: 0x00080080
	public T Current
	{
		get
		{
			return this.enumerator.Current;
		}
	}

	// Token: 0x06002132 RID: 8498 RVA: 0x00081E90 File Offset: 0x00080090
	public void Reset()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06002133 RID: 8499 RVA: 0x00081E98 File Offset: 0x00080098
	public void Dispose()
	{
		this.enumerator.Dispose();
	}

	// Token: 0x04000F94 RID: 3988
	private HashSet<T>.Enumerator enumerator;
}
