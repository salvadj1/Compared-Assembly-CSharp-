using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000415 RID: 1045
public struct ODBForwardEnumerator<T> : IDisposable, IEnumerator, global::ODBEnumerator<T>, IEnumerator<T> where T : Object
{
	// Token: 0x060024A4 RID: 9380 RVA: 0x000874EC File Offset: 0x000856EC
	public ODBForwardEnumerator(global::ODBNode<T> node)
	{
		this.sib.has = true;
		this.sib.item = node;
		this.Current = (T)((object)null);
	}

	// Token: 0x060024A5 RID: 9381 RVA: 0x00087520 File Offset: 0x00085720
	public ODBForwardEnumerator(global::ODBList<T> list)
	{
		this = new global::ODBForwardEnumerator<T>(list.first);
	}

	// Token: 0x060024A6 RID: 9382 RVA: 0x00087530 File Offset: 0x00085730
	public ODBForwardEnumerator(global::ODBSibling<T> sibling)
	{
		this.sib = sibling;
		this.Current = (T)((object)null);
	}

	// Token: 0x17000886 RID: 2182
	// (get) Token: 0x060024A7 RID: 9383 RVA: 0x00087548 File Offset: 0x00085748
	T global::ODBEnumerator<T>.ExplicitCurrent
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x17000887 RID: 2183
	// (get) Token: 0x060024A8 RID: 9384 RVA: 0x00087550 File Offset: 0x00085750
	T IEnumerator<T>.Current
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x17000888 RID: 2184
	// (get) Token: 0x060024A9 RID: 9385 RVA: 0x00087558 File Offset: 0x00085758
	object IEnumerator.Current
	{
		get
		{
			return this.Current;
		}
	}

	// Token: 0x060024AA RID: 9386 RVA: 0x00087568 File Offset: 0x00085768
	void IEnumerator.Reset()
	{
		throw new NotSupportedException();
	}

	// Token: 0x060024AB RID: 9387 RVA: 0x00087570 File Offset: 0x00085770
	public bool MoveNext()
	{
		if (this.sib.has)
		{
			global::ODBNode<T> item = this.sib.item;
			this.Current = item.self;
			this.sib = item.n;
			return true;
		}
		return false;
	}

	// Token: 0x060024AC RID: 9388 RVA: 0x000875B4 File Offset: 0x000857B4
	public void Dispose()
	{
		this.sib = default(global::ODBSibling<T>);
		this.Current = (T)((object)null);
	}

	// Token: 0x060024AD RID: 9389 RVA: 0x000875DC File Offset: 0x000857DC
	public IEnumerator<T> ToGeneric()
	{
		global::ODBForwardEnumerator<T> odbforwardEnumerator = this;
		return global::ODBCachedEnumerator<T, global::ODBForwardEnumerator<T>>.Cache(ref odbforwardEnumerator);
	}

	// Token: 0x04001103 RID: 4355
	private global::ODBSibling<T> sib;

	// Token: 0x04001104 RID: 4356
	public T Current;
}
