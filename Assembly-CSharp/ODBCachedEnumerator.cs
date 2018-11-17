using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041D RID: 1053
public class ODBCachedEnumerator<T, TEnumerator> : IDisposable, IEnumerator, global::ODBEnumerator<T>, IEnumerator<T> where T : Object where TEnumerator : struct, global::ODBEnumerator<T>
{
	// Token: 0x060024D1 RID: 9425 RVA: 0x00087924 File Offset: 0x00085B24
	private ODBCachedEnumerator(ref TEnumerator enumerator)
	{
		this.enumerator = enumerator;
	}

	// Token: 0x1700088C RID: 2188
	// (get) Token: 0x060024D2 RID: 9426 RVA: 0x00087938 File Offset: 0x00085B38
	T global::ODBEnumerator<T>.ExplicitCurrent
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x1700088D RID: 2189
	// (get) Token: 0x060024D3 RID: 9427 RVA: 0x00087940 File Offset: 0x00085B40
	object IEnumerator.Current
	{
		get
		{
			throw new NotSupportedException("You must use the IEnumerator<> interface. as dispose is entirely neccisary");
		}
	}

	// Token: 0x060024D4 RID: 9428 RVA: 0x0008794C File Offset: 0x00085B4C
	IEnumerator<T> global::ODBEnumerator<T>.ToGeneric()
	{
		return this;
	}

	// Token: 0x1700088E RID: 2190
	// (get) Token: 0x060024D5 RID: 9429 RVA: 0x00087950 File Offset: 0x00085B50
	public T Current
	{
		get
		{
			return this.enumerator.ExplicitCurrent;
		}
	}

	// Token: 0x060024D6 RID: 9430 RVA: 0x00087964 File Offset: 0x00085B64
	public bool MoveNext()
	{
		return this.enumerator.MoveNext();
	}

	// Token: 0x060024D7 RID: 9431 RVA: 0x00087978 File Offset: 0x00085B78
	public void Reset()
	{
		this.enumerator.Reset();
	}

	// Token: 0x060024D8 RID: 9432 RVA: 0x0008798C File Offset: 0x00085B8C
	public void Dispose()
	{
		if (!this.disposed)
		{
			this.disposed = true;
			this.next = global::ODBCachedEnumerator<T, TEnumerator>.recycle;
			global::ODBCachedEnumerator<T, TEnumerator>.recycle = this;
			this.enumerator.Dispose();
			this.enumerator = default(TEnumerator);
		}
	}

	// Token: 0x060024D9 RID: 9433 RVA: 0x000879DC File Offset: 0x00085BDC
	public static IEnumerator<T> Cache(ref TEnumerator enumerator)
	{
		if (global::ODBCachedEnumerator<T, TEnumerator>.recycle == null)
		{
			return new global::ODBCachedEnumerator<T, TEnumerator>(ref enumerator);
		}
		global::ODBCachedEnumerator<T, TEnumerator> odbcachedEnumerator = global::ODBCachedEnumerator<T, TEnumerator>.recycle;
		global::ODBCachedEnumerator<T, TEnumerator>.recycle = odbcachedEnumerator.next;
		odbcachedEnumerator.disposed = false;
		odbcachedEnumerator.enumerator = enumerator;
		odbcachedEnumerator.next = null;
		return odbcachedEnumerator;
	}

	// Token: 0x0400110D RID: 4365
	private global::ODBCachedEnumerator<T, TEnumerator> next;

	// Token: 0x0400110E RID: 4366
	private static global::ODBCachedEnumerator<T, TEnumerator> recycle;

	// Token: 0x0400110F RID: 4367
	private TEnumerator enumerator;

	// Token: 0x04001110 RID: 4368
	private bool disposed;
}
