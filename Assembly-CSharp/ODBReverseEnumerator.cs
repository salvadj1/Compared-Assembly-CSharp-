using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000369 RID: 873
public struct ODBReverseEnumerator<T> : IDisposable, IEnumerator, ODBEnumerator<T>, IEnumerator<T> where T : Object
{
	// Token: 0x0600214C RID: 8524 RVA: 0x000821FC File Offset: 0x000803FC
	public ODBReverseEnumerator(ODBNode<T> node)
	{
		this.sib.has = true;
		this.sib.item = node;
		this.Current = (T)((object)null);
	}

	// Token: 0x0600214D RID: 8525 RVA: 0x00082230 File Offset: 0x00080430
	public ODBReverseEnumerator(ODBList<T> list)
	{
		this = new ODBReverseEnumerator<T>(list.last);
	}

	// Token: 0x0600214E RID: 8526 RVA: 0x00082240 File Offset: 0x00080440
	public ODBReverseEnumerator(ODBSibling<T> sibling)
	{
		this.sib = sibling;
		this.Current = (T)((object)null);
	}

	// Token: 0x1700082B RID: 2091
	// (get) Token: 0x0600214F RID: 8527 RVA: 0x00082258 File Offset: 0x00080458
	T ODBEnumerator<T>.ExplicitCurrent
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x1700082C RID: 2092
	// (get) Token: 0x06002150 RID: 8528 RVA: 0x00082260 File Offset: 0x00080460
	T IEnumerator<T>.Current
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x1700082D RID: 2093
	// (get) Token: 0x06002151 RID: 8529 RVA: 0x00082268 File Offset: 0x00080468
	object IEnumerator.Current
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x06002152 RID: 8530 RVA: 0x00082278 File Offset: 0x00080478
	void IEnumerator.Reset()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06002153 RID: 8531 RVA: 0x00082280 File Offset: 0x00080480
	public bool MoveNext()
	{
		if (this.sib.has)
		{
			ODBNode<T> item = this.sib.item;
			this.Current = item.self;
			this.sib = item.p;
			return true;
		}
		return false;
	}

	// Token: 0x06002154 RID: 8532 RVA: 0x000822C4 File Offset: 0x000804C4
	public void Dispose()
	{
		this.sib = default(ODBSibling<T>);
		this.Current = (T)((object)null);
	}

	// Token: 0x06002155 RID: 8533 RVA: 0x000822EC File Offset: 0x000804EC
	public IEnumerator<T> ToGeneric()
	{
		ODBReverseEnumerator<T> odbreverseEnumerator = this;
		return ODBCachedEnumerator<T, ODBReverseEnumerator<T>>.Cache(ref odbreverseEnumerator);
	}

	// Token: 0x04000F9F RID: 3999
	private ODBSibling<T> sib;

	// Token: 0x04000FA0 RID: 4000
	public T Current;
}
