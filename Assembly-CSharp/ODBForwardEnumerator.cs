using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000368 RID: 872
public struct ODBForwardEnumerator<T> : IDisposable, IEnumerator, ODBEnumerator<T>, IEnumerator<T> where T : Object
{
	// Token: 0x06002142 RID: 8514 RVA: 0x000820F0 File Offset: 0x000802F0
	public ODBForwardEnumerator(ODBNode<T> node)
	{
		this.sib.has = true;
		this.sib.item = node;
		this.Current = (T)((object)null);
	}

	// Token: 0x06002143 RID: 8515 RVA: 0x00082124 File Offset: 0x00080324
	public ODBForwardEnumerator(ODBList<T> list)
	{
		this = new ODBForwardEnumerator<T>(list.first);
	}

	// Token: 0x06002144 RID: 8516 RVA: 0x00082134 File Offset: 0x00080334
	public ODBForwardEnumerator(ODBSibling<T> sibling)
	{
		this.sib = sibling;
		this.Current = (T)((object)null);
	}

	// Token: 0x17000828 RID: 2088
	// (get) Token: 0x06002145 RID: 8517 RVA: 0x0008214C File Offset: 0x0008034C
	T ODBEnumerator<T>.ExplicitCurrent
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x17000829 RID: 2089
	// (get) Token: 0x06002146 RID: 8518 RVA: 0x00082154 File Offset: 0x00080354
	T IEnumerator<T>.Current
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x1700082A RID: 2090
	// (get) Token: 0x06002147 RID: 8519 RVA: 0x0008215C File Offset: 0x0008035C
	object IEnumerator.Current
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x06002148 RID: 8520 RVA: 0x0008216C File Offset: 0x0008036C
	void IEnumerator.Reset()
	{
		throw new NotSupportedException();
	}

	// Token: 0x06002149 RID: 8521 RVA: 0x00082174 File Offset: 0x00080374
	public bool MoveNext()
	{
		if (this.sib.has)
		{
			ODBNode<T> item = this.sib.item;
			this.Current = item.self;
			this.sib = item.n;
			return true;
		}
		return false;
	}

	// Token: 0x0600214A RID: 8522 RVA: 0x000821B8 File Offset: 0x000803B8
	public void Dispose()
	{
		this.sib = default(ODBSibling<T>);
		this.Current = (T)((object)null);
	}

	// Token: 0x0600214B RID: 8523 RVA: 0x000821E0 File Offset: 0x000803E0
	public IEnumerator<T> ToGeneric()
	{
		ODBForwardEnumerator<T> odbforwardEnumerator = this;
		return ODBCachedEnumerator<T, ODBForwardEnumerator<T>>.Cache(ref odbforwardEnumerator);
	}

	// Token: 0x04000F9D RID: 3997
	private ODBSibling<T> sib;

	// Token: 0x04000F9E RID: 3998
	public T Current;
}
