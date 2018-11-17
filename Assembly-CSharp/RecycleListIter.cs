using System;
using System.Collections.Generic;

// Token: 0x02000425 RID: 1061
public struct RecycleListIter<T>
{
	// Token: 0x06002523 RID: 9507 RVA: 0x00088668 File Offset: 0x00086868
	internal RecycleListIter(List<T>.Enumerator enumerator)
	{
		this.enumerator = enumerator;
	}

	// Token: 0x06002524 RID: 9508 RVA: 0x00088674 File Offset: 0x00086874
	public bool MoveNext()
	{
		return this.enumerator.MoveNext();
	}

	// Token: 0x17000898 RID: 2200
	// (get) Token: 0x06002525 RID: 9509 RVA: 0x00088684 File Offset: 0x00086884
	public T Current
	{
		get
		{
			return this.enumerator.Current;
		}
	}

	// Token: 0x06002526 RID: 9510 RVA: 0x00088694 File Offset: 0x00086894
	public void Dispose()
	{
		this.enumerator.Dispose();
	}

	// Token: 0x04001120 RID: 4384
	private List<T>.Enumerator enumerator;
}
