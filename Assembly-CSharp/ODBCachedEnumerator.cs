using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000370 RID: 880
public class ODBCachedEnumerator<T, TEnumerator> : IDisposable, IEnumerator, ODBEnumerator<T>, IEnumerator<T> where T : Object where TEnumerator : struct, ODBEnumerator<T>
{
	// Token: 0x0600216F RID: 8559 RVA: 0x00082528 File Offset: 0x00080728
	private ODBCachedEnumerator(ref TEnumerator enumerator)
	{
		this.enumerator = enumerator;
	}

	// Token: 0x1700082E RID: 2094
	// (get) Token: 0x06002170 RID: 8560 RVA: 0x0008253C File Offset: 0x0008073C
	T ODBEnumerator<T>.ExplicitCurrent
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x1700082F RID: 2095
	// (get) Token: 0x06002171 RID: 8561 RVA: 0x00082544 File Offset: 0x00080744
	object IEnumerator.Current
	{
		get
		{
			throw new NotSupportedException("You must use the IEnumerator<> interface. as dispose is entirely neccisary");
		}
	}

	// Token: 0x06002172 RID: 8562 RVA: 0x00082550 File Offset: 0x00080750
	IEnumerator<T> ODBEnumerator<T>.ToGeneric()
	{
		return this;
	}

	// Token: 0x17000830 RID: 2096
	// (get) Token: 0x06002173 RID: 8563 RVA: 0x00082554 File Offset: 0x00080754
	public T Current
	{
		get
		{
			return this.enumerator.ExplicitCurrent;
		}
	}

	// Token: 0x06002174 RID: 8564 RVA: 0x00082568 File Offset: 0x00080768
	public bool MoveNext()
	{
		return this.enumerator.MoveNext();
	}

	// Token: 0x06002175 RID: 8565 RVA: 0x0008257C File Offset: 0x0008077C
	public void Reset()
	{
		this.enumerator.Reset();
	}

	// Token: 0x06002176 RID: 8566 RVA: 0x00082590 File Offset: 0x00080790
	public void Dispose()
	{
		if (!this.disposed)
		{
			this.disposed = true;
			this.next = ODBCachedEnumerator<T, TEnumerator>.recycle;
			ODBCachedEnumerator<T, TEnumerator>.recycle = this;
			this.enumerator.Dispose();
			this.enumerator = default(TEnumerator);
		}
	}

	// Token: 0x06002177 RID: 8567 RVA: 0x000825E0 File Offset: 0x000807E0
	public static IEnumerator<T> Cache(ref TEnumerator enumerator)
	{
		if (ODBCachedEnumerator<T, TEnumerator>.recycle == null)
		{
			return new ODBCachedEnumerator<T, TEnumerator>(ref enumerator);
		}
		ODBCachedEnumerator<T, TEnumerator> odbcachedEnumerator = ODBCachedEnumerator<T, TEnumerator>.recycle;
		ODBCachedEnumerator<T, TEnumerator>.recycle = odbcachedEnumerator.next;
		odbcachedEnumerator.disposed = false;
		odbcachedEnumerator.enumerator = enumerator;
		odbcachedEnumerator.next = null;
		return odbcachedEnumerator;
	}

	// Token: 0x04000FA7 RID: 4007
	private ODBCachedEnumerator<T, TEnumerator> next;

	// Token: 0x04000FA8 RID: 4008
	private static ODBCachedEnumerator<T, TEnumerator> recycle;

	// Token: 0x04000FA9 RID: 4009
	private TEnumerator enumerator;

	// Token: 0x04000FAA RID: 4010
	private bool disposed;
}
