using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000416 RID: 1046
public struct ODBReverseEnumerator<T> : IDisposable, IEnumerator, global::ODBEnumerator<T>, IEnumerator<T> where T : Object
{
	// Token: 0x060024AE RID: 9390 RVA: 0x000875F8 File Offset: 0x000857F8
	public ODBReverseEnumerator(global::ODBNode<T> node)
	{
		this.sib.has = true;
		this.sib.item = node;
		this.Current = (T)((object)null);
	}

	// Token: 0x060024AF RID: 9391 RVA: 0x0008762C File Offset: 0x0008582C
	public ODBReverseEnumerator(global::ODBList<T> list)
	{
		this = new global::ODBReverseEnumerator<T>(list.last);
	}

	// Token: 0x060024B0 RID: 9392 RVA: 0x0008763C File Offset: 0x0008583C
	public ODBReverseEnumerator(global::ODBSibling<T> sibling)
	{
		this.sib = sibling;
		this.Current = (T)((object)null);
	}

	// Token: 0x17000889 RID: 2185
	// (get) Token: 0x060024B1 RID: 9393 RVA: 0x00087654 File Offset: 0x00085854
	T global::ODBEnumerator<T>.ExplicitCurrent
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x1700088A RID: 2186
	// (get) Token: 0x060024B2 RID: 9394 RVA: 0x0008765C File Offset: 0x0008585C
	T IEnumerator<T>.Current
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x1700088B RID: 2187
	// (get) Token: 0x060024B3 RID: 9395 RVA: 0x00087664 File Offset: 0x00085864
	object IEnumerator.Current
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x060024B4 RID: 9396 RVA: 0x00087674 File Offset: 0x00085874
	void IEnumerator.Reset()
	{
		throw new NotSupportedException();
	}

	// Token: 0x060024B5 RID: 9397 RVA: 0x0008767C File Offset: 0x0008587C
	public bool MoveNext()
	{
		if (this.sib.has)
		{
			global::ODBNode<T> item = this.sib.item;
			this.Current = item.self;
			this.sib = item.p;
			return true;
		}
		return false;
	}

	// Token: 0x060024B6 RID: 9398 RVA: 0x000876C0 File Offset: 0x000858C0
	public void Dispose()
	{
		this.sib = default(global::ODBSibling<T>);
		this.Current = (T)((object)null);
	}

	// Token: 0x060024B7 RID: 9399 RVA: 0x000876E8 File Offset: 0x000858E8
	public IEnumerator<T> ToGeneric()
	{
		global::ODBReverseEnumerator<T> odbreverseEnumerator = this;
		return global::ODBCachedEnumerator<T, global::ODBReverseEnumerator<T>>.Cache(ref odbreverseEnumerator);
	}

	// Token: 0x04001105 RID: 4357
	private global::ODBSibling<T> sib;

	// Token: 0x04001106 RID: 4358
	public T Current;
}
