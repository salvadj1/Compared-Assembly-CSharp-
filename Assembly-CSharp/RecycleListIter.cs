using System;
using System.Collections.Generic;

// Token: 0x02000378 RID: 888
public struct RecycleListIter<T>
{
	// Token: 0x060021C1 RID: 8641 RVA: 0x0008326C File Offset: 0x0008146C
	internal RecycleListIter(List<T>.Enumerator enumerator)
	{
		this.enumerator = enumerator;
	}

	// Token: 0x060021C2 RID: 8642 RVA: 0x00083278 File Offset: 0x00081478
	public bool MoveNext()
	{
		return this.enumerator.MoveNext();
	}

	// Token: 0x1700083A RID: 2106
	// (get) Token: 0x060021C3 RID: 8643 RVA: 0x00083288 File Offset: 0x00081488
	public T Current
	{
		get
		{
			return this.enumerator.Current;
		}
	}

	// Token: 0x060021C4 RID: 8644 RVA: 0x00083298 File Offset: 0x00081498
	public void Dispose()
	{
		this.enumerator.Dispose();
	}

	// Token: 0x04000FBA RID: 4026
	private List<T>.Enumerator enumerator;
}
