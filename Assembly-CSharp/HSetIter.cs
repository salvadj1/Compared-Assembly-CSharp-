using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000410 RID: 1040
public struct HSetIter<T> : IDisposable, IEnumerator, IEnumerator<T>
{
	// Token: 0x06002490 RID: 9360 RVA: 0x0008724C File Offset: 0x0008544C
	public HSetIter(HashSet<T>.Enumerator enumerator)
	{
		this.enumerator = enumerator;
	}

	// Token: 0x17000883 RID: 2179
	// (get) Token: 0x06002491 RID: 9361 RVA: 0x00087258 File Offset: 0x00085458
	object IEnumerator.Current
	{
		get
		{
			return this.enumerator.Current;
		}
	}

	// Token: 0x06002492 RID: 9362 RVA: 0x0008726C File Offset: 0x0008546C
	public bool MoveNext()
	{
		return this.enumerator.MoveNext();
	}

	// Token: 0x17000884 RID: 2180
	// (get) Token: 0x06002493 RID: 9363 RVA: 0x0008727C File Offset: 0x0008547C
	public T Current
	{
		get
		{
			return this.enumerator.Current;
		}
	}

	// Token: 0x06002494 RID: 9364 RVA: 0x0008728C File Offset: 0x0008548C
	public void Reset()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06002495 RID: 9365 RVA: 0x00087294 File Offset: 0x00085494
	public void Dispose()
	{
		this.enumerator.Dispose();
	}

	// Token: 0x040010FA RID: 4346
	private HashSet<T>.Enumerator enumerator;
}
